using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Data;
using ServiceOrderManager.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceOrderManager.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<SystemUser> _userManager;

        public AppointmentsController(AppDbContext context, UserManager<SystemUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            // Carrega os profissionais para preencher o select no modal de criação
            var professionals = await _userManager.Users.Where(u => u.Enabled && u.UserRole == "Technician").ToListAsync();
            ViewBag.Professionals = new SelectList(professionals, "Id", "FirstName");

            return View();
        }






        // GET: Appointments/GetDetails/5
        [HttpGet]
        public async Task<IActionResult> GetDetails(int id)
        {
            var appointment = await _context.ServiceAppointments
                .Include(a => a.AssignedTechnician)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            // Retorna os dados mapeados para o JavaScript preencher o modal
            return Json(new
            {
                id = appointment.Id,
                title = appointment.Title,
                description = appointment.Description,
                clientName = appointment.ClientAssigned?.Name,
                clientPhone = appointment.ClientAssigned?.PrimariPhone,
                startTime = appointment.StartTime.ToString("yyyy-MM-ddTHH:mm"),
                endTime = appointment.EndTime.ToString("yyyy-MM-ddTHH:mm"),
                status = (int)appointment.Status,
                assignedUserId = appointment.AssignedTechnicianId
            });
        }

        // POST: Appointments/UpdateStatusOrDelete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatusOrDelete(int id, AppointmentStatus status, string actionType)
        {
            var appointment = await _context.ServiceAppointments.FindAsync(id);
            if (appointment == null)
            {
                return Json(new { success = false, message = "Agendamento não encontrado." });
            }

            if (actionType == "delete")
            {
                _context.ServiceAppointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Agendamento excluído com sucesso!" });
            }

            if (actionType == "update")
            {
                // Se mudou o status para Cancelado, pula a validação de conflito
                if (status != AppointmentStatus.Cancelled)
                {
                    // Valida choque de horário (garante que não conflita com OUTROS agendamentos ativos)
                    bool hasConflict = await _context.ServiceAppointments
                        .AnyAsync(a => a.Id != id &&
                                       a.AssignedTechnicianId == appointment.AssignedTechnicianId &&
                                       a.Status != AppointmentStatus.Cancelled &&
                                       appointment.StartTime < a.EndTime &&
                                       appointment.EndTime > a.StartTime);

                    if (hasConflict)
                    {
                        return Json(new { success = false, message = "Não é possível alterar: Há conflito de horário para este profissional." });
                    }
                }

                appointment.Status = status;
                _context.Update(appointment);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Status atualizado com sucesso!" });
            }

            return Json(new { success = false, message = "Ação inválida." });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceAppointment appointment)
        {
            // 1. Validação básica de consistência: O término não pode ser antes do início
            if (appointment.EndTime <= appointment.StartTime)
            {
                return Json(new
                {
                    success = false,
                    message = "Erro: A data/hora de término deve ser posterior à data/hora de início."
                });
            }

            if (ModelState.IsValid)
            {
                // 2. Regra de Negócio: Verifica se há choque de horários para o mesmo profissional
                // Desconsidera agendamentos com status 'Cancelled' (Cancelado)
                bool hasConflict = await _context.ServiceAppointments
                    .AnyAsync(a => a.AssignedTechnicianId == appointment.AssignedTechnicianId &&
                                   a.Status != AppointmentStatus.Cancelled &&
                                   appointment.StartTime < a.EndTime &&
                                   appointment.EndTime > a.StartTime);

                if (hasConflict)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Conflito de Agenda: O profissional selecionado já possui um serviço agendado neste intervalo de tempo."
                    });
                }

                // 3. Salva se não houver conflito
                _context.Add(appointment);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Agendamento realizado com sucesso!" });
            }

            return Json(new { success = false, message = "Erro ao validar os dados do formulário." });
        }




        // GET: Appointments/GetCalendarEvents
        [HttpGet]
        public async Task<JsonResult> GetCalendarEvents(DateTime start, DateTime end, int? assignedUserId = null)
        {
            // Inicia a query buscando os agendamentos no período visível do calendário
            var query = _context.ServiceAppointments
                .Include(a => a.AssignedTechnicianId)
                .Where(a => a.StartTime >= start && a.EndTime <= end);

            // Se um profissional específico foi selecionado no filtro, aplica a restrição
            if (assignedUserId == null)
            {
                query = query.Where(a => a.AssignedTechnicianId == assignedUserId);
            }

            var appointments = await query.ToListAsync();

            // Mapeia para o formato esperado pelo FullCalendar.js
            var events = appointments.Select(a => new
            {
                id = a.Id,
                title = $"{a.ClientAssigned.Name} - {a.Title}",
                start = a.StartTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = a.EndTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                description = a.Description,
                color = GetStatusColor(a.Status),
                allDay = false
            });

            return Json(events);
        }


        // POST: Appointments/CreateAsync (Chamada AJAX do Modal)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApp(ServiceAppointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Agendamento realizado com sucesso!" });
            }
            return Json(new { success = false, message = "Erro ao validar os dados do formulário." });
        }

        // Função auxiliar para definir cores baseadas no status
        private string GetStatusColor(AppointmentStatus status)
        {
            return status switch
            {
                AppointmentStatus.Scheduled => "#0d6efd", // Azul Bootstrap
                AppointmentStatus.Confirmed => "#198754", // Verde
                AppointmentStatus.Completed => "#6c757d", // Cinza
                AppointmentStatus.Cancelled => "#dc3545", // Vermelho
                _ => "#0d6efd"
            };
        }
    }
}
