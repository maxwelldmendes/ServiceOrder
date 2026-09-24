using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Models.ViewModels;
using ServiceOrderManager.Data;

namespace ServiceOrderManager.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // Exibe a página da agenda
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await CarregarCombosAsync();

            return View();
        }

        // Carrega clientes e profissionais nos Selects
        private async Task CarregarCombosAsync()
        {
           
            var clients = await _context.Client
                .OrderBy(x => x.Name)
                .ToListAsync();
           
            var professionals = await _context.Users
                .Where(x => x.UserRole == "Technician") // ajuste conforme seu modelo
                .OrderBy(x => x.FirstName)
                .ToListAsync();
           
            ViewBag.Clients = new SelectList(
                clients,
                "Id",
                "Name"
            );
            
            ViewBag.Professionals = new SelectList(
                professionals,
                "Id",
                "FirstName"
            );
           
        }

        // Retorna os eventos para o FullCalendar
        [HttpGet]
        public async Task<IActionResult> GetCalendarEvents(
            DateTime start,
            DateTime end,
            string? assignedUserId)
        {
            var query = _context.Appointments
                .AsNoTracking()
                .Include(x => x.Client)
                .Include(x => x.AssignedUser)
                .Where(x =>
                    x.StartTime < end &&
                    x.EndTime > start
                );

            if (!string.IsNullOrWhiteSpace(assignedUserId))
            {
                query = query.Where(x =>
                    x.AssignedUserId == assignedUserId
                );
            }

            var appointments = await query.ToListAsync();

            var events = appointments.Select(x => new
            {
                id = x.Id,
                title = string.IsNullOrWhiteSpace(x.Title)
                    ? x.Client.Name
                    : x.Title,

                start = x.StartTime,
                end = x.EndTime,

                extendedProps = new
                {
                    client = x.Client.Name,
                    professional = x.AssignedUser.FirstName,
                    status = x.Status
                }
            });

            return Json(events);
        }

        // Retorna os dados de um agendamento para edição
        [HttpGet]
        public async Task<IActionResult> GetDetails(int id)
        {
            var appointment = await _context.Appointments
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (appointment == null)
            {
                return NotFound(new
                {
                    message = "Agendamento não encontrado."
                });
            }

            return Json(new
            {
                id = appointment.Id,
                clientId = appointment.ClientId,
                clientName = appointment.Client?.Name,
                title = appointment.Title,
                assignedUserId = appointment.AssignedUserId,
                startTime = appointment.StartTime.ToString("yyyy-MM-ddTHH:mm"),
                endTime = appointment.EndTime.ToString("yyyy-MM-ddTHH:mm"),
                description = appointment.Description,
                status = (int)appointment.Status
            });
        }

        // Cria um novo agendamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Verifique os campos obrigatórios."
                });
            }

            var clientExists = await _context.Client
                .AnyAsync(x => x.Id == model.ClientId);

            if (!clientExists)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Cliente inválido."
                });
            }

            var professionalExists = await _context.Users
                .AnyAsync(x => x.Id == model.AssignedUserId);

            if (!professionalExists)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Profissional inválido."
                });
            }

            if (model.EndTime <= model.StartTime)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "O término deve ser posterior ao início."
                });
            }

            var appointment = new Appointment
            {
                ClientId = model.ClientId,
                Title = model.Title,
                AssignedUserId = model.AssignedUserId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Description = model.Description,
                Status = AppointmentStatus.Scheduled
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "Agendamento criado com sucesso."
            });
        }

        // Atualiza o status ou exclui o agendamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatusOrDelete(
                                                AppointmentViewModel model,
                                                int status,
                                                string actionType)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (appointment == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Agendamento não encontrado."
                });
            }

            if (actionType == "delete")
            {
                _context.Appointments.Remove(appointment);
            }
            else if (actionType == "update")
            {
                appointment.ClientId = model.ClientId;
                appointment.Title = model.Title;
                appointment.AssignedUserId = model.AssignedUserId;
                appointment.StartTime = model.StartTime;
                appointment.EndTime = model.EndTime;
                appointment.Description = model.Description;
                appointment.Status = (AppointmentStatus)status;
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Ação inválida."
                });
            }

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = actionType == "delete"
                    ? "Agendamento excluído com sucesso."
                    : "Agendamento atualizado com sucesso."
            });
        }


        private static string GetStatusColor(AppointmentStatus status)
        {
            return status switch
            {
                AppointmentStatus.Scheduled => "#0d6efd", // azul
                AppointmentStatus.Confirmed => "#198754", // verde
                AppointmentStatus.Completed => "#6c757d", // cinza
                AppointmentStatus.Canceled => "#dc3545",  // vermelho
                _ => "#0d6efd"
            };
        }
    }
}
