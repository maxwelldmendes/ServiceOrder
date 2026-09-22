using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceOrderManager.Data; // Ajuste para o namespace do seu DbContext
using ServiceOrderManager.Mappings;
using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;

namespace ServiceOrderManager.Controllers
{
    [Authorize]
    public class TechnicianController : Controller
    {
        private readonly AppDbContext _context;

        public TechnicianController(AppDbContext context)
        {
            _context = context;
        }

        /*----------------------------------------------------------------
         * Metodo para popular o Index.cshtml de Technician
         ----------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userTechsVM = await (
                from tech in _context.Technician
                join user in _context.SystemUser
                    on tech.UserId equals user.Id
                select new UserTechnicianViewModel
                {
                    Id = tech.Id,
                    SelectedUserId = user.Id,
                    FirstName = user.FirstName,
                    Middlename = user.MiddleName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Skills = tech.Skills,
                    Enabled = tech.Enabled,
                    UserRole = user.UserRole
                }
            ).ToListAsync();

            return View(userTechsVM);
        }

        /*--------------------------------------------------------------------------
         * Metodo para abrir a TechnicianViewodel ao clicar no botao New Technician
         -------------------------------------------------------------------------*/
        // GET: Technician/Create
        [HttpGet]
        public async Task<IActionResult> CreateTechnician()
        {
            // Inicializa o ViewModel com os objetos de endereço instanciados
            var model = new UserTechnicianViewModel();

            await PopulateAvailableUsersAsync(model);

            return View(model);
        }

        // GET: Ajax/JavaScript request
        [HttpGet]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Return only the data the view needs to populate the fields
            return Json(new
            {
                firstName = user.FirstName,
                middleName = user.MiddleName,
                lastName = user.LastName,
                userName = user.UserName,
                email = user.Email,
                phoneNumber = user.PhoneNumber,
                userRole = user.UserRole
            });
        }

        // POST: Technician/Create
        [HttpPost]
        public async Task<IActionResult> CreateTechnician(UserTechnicianViewModel viewModel)
        {
            var tecnicianModel = new Technician()
            {
                Id = viewModel.Id,
                Skills = viewModel.Skills,
                Enabled = viewModel.Enabled,
                UserId = viewModel.SelectedUserId
            };

            _context.Add(tecnicianModel);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Technician inserted in database!";
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para buscar os usuários elegíveis
        private async Task PopulateAvailableUsersAsync(UserTechnicianViewModel model)
        {
            // 1. Pega IDs de usuários que já são Técnicos para excluí-los (relação 0..1 para 1)
            var existingTechnicianUserIds = await _context.Technician
                .Select(t => t.UserId)
                .ToListAsync();

            // 2. Busca usuários com a Role certa (ex: "Technician") e que estão livres
            // Nota: Ajuste a lógica de Roles conforme o seu sistema de Identity (ex: usando UserManager)
            var eligibleUsers = await _context.SystemUser
                .Where(u => !existingTechnicianUserIds.Contains(u.Id))
                .Where(u => u.UserRole == "Technician") // Se tiver tabela de junção
                .Select(u => new SystemUserLookupViewModel
                {
                    Id = u.Id,
                    FullName = u.UserName ?? string.Empty
                })
                .ToListAsync();

            model.AvailableUsers = new SelectList(eligibleUsers, "Id", "FullName");
        }
    }
}
