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
            var users = await _context.Users
                .Where(u => u.UserRole == "Technician")
                .ToListAsync();

            var usersTechsVM = new List<UserTechnicianViewModel>();

            foreach (var user in users) 
            {
                var technician = await FindTechnician(user.Id);

                if (technician != null) 
                {
                    usersTechsVM.Add(new UserTechnicianViewModel
                    {
                        Id = technician.Id,
                        SelectedUserId = user.Id,
                        FirstName = user.FirstName,
                        Middlename = user.MiddleName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        UserEmail = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Skills = technician.Skills,
                        Enabled = technician.Enabled,
                        UserRole = user.UserRole
                    });
                }
            }
            return View(usersTechsVM);
        }

        private async Task<Technician> FindTechnician(string userId)
        {
            var techs = await _context.Technician.FirstOrDefaultAsync(t => t.UserId == userId);

            return techs!;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTechnician(UserTechnicianViewModel viewModel)
        {
            var tecnicianModel = new Technician()
            {
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

        /*------------------------------------------------------------------------------------------------------
         * Executa a apresentacao dos dados de User e Technician pelo users.Id
         -----------------------------------------------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> DetailTechnician(int id)
        {
            // Quando faco esta leitura, automaticamente carrego o ojjeto SystemUser 
            // relacionado a este Technician
            var technician = await _context.Technician
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.Id == id);


            if (technician == null)
                return NotFound();

            //Console.WriteLine(""technician.User.FirstName);

            var usersTechsVM = new UserTechnicianViewModel();

            usersTechsVM.Id = technician.Id;
            usersTechsVM.SelectedUserId = technician.UserId;
            usersTechsVM.FirstName = technician.User!.FirstName;
            usersTechsVM.Middlename = technician.User.MiddleName;
            usersTechsVM.LastName = technician.User.LastName;
            usersTechsVM.UserName = technician.User.UserName;
            usersTechsVM.UserEmail = technician.User.Email;
            usersTechsVM.PhoneNumber = technician.User.PhoneNumber;
            usersTechsVM.Skills = technician.Skills;
            usersTechsVM.Enabled = technician.Enabled;
            usersTechsVM.UserRole = technician.User.UserRole;
            //
            
            return View(usersTechsVM);
        }
        /*----------------------------------------------------------------------------------------------------------
         * 
         ---------------------------------------------------------------------------------------------------------*/ 
        [HttpGet]
        public async Task<IActionResult> EditTechnician(int id)
        {
            // Quando faco esta leitura, automaticamente carrego o ojjeto SystemUser 
            // relacionado a este Technician
            var technician = await _context.Technician
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.Id == id);


            if (technician == null)
                return NotFound();

            //Console.WriteLine(""technician.User.FirstName);

            var usersTechsVM = new UserTechnicianViewModel();

            usersTechsVM.Id = technician.Id;
            usersTechsVM.SelectedUserId = technician.UserId;
            usersTechsVM.FirstName = technician.User!.FirstName;
            usersTechsVM.Middlename = technician.User.MiddleName;
            usersTechsVM.LastName = technician.User.LastName;
            usersTechsVM.UserName = technician.User.UserName;
            usersTechsVM.UserEmail = technician.User.Email;
            usersTechsVM.PhoneNumber = technician.User.PhoneNumber;
            usersTechsVM.Skills = technician.Skills;
            usersTechsVM.Enabled = technician.Enabled;
            usersTechsVM.UserRole = technician.User.UserRole;
            //

            return View(usersTechsVM);
        }

        /*-----------------------------------------------------------------------------------------
         * Salva as alteracoes no banco de dados 
         ----------------------------------------------------------------------------------------*/ 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTechnician(UserTechnicianViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var techs = new Technician()
            {
                Id = model.Id,
                UserId = model.SelectedUserId,
                Skills = model.Skills,
                Enabled = model.Enabled
            };

            _context.Update(techs);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Technician inserted in database!";
            return RedirectToAction(nameof(Index));
        }

        /*******************************************************************************************
         * Delete Technician
         -----------------------------------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> DeleteTechnician(int id)
        {
            var technician = await _context.Technician.FindAsync(id);
         
            if (technician == null)
                return NotFound();

            _context.Remove(technician);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Technician removed in database!";
            return RedirectToAction(nameof(Index));
        }
    }
}
