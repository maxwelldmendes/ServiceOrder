using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceOrderManager.Data; // Ajuste para o namespace do seu DbContext
using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;
using ServiceOrderManager.Mappings;
using ServiceOrderManager.Repositories;

namespace ServiceOrderManager.Controllers
{
    [Authorize]
    public class TechnicianController : Controller
    {
        private readonly ITechnicianRepository _repository;
        private readonly SignInManager<SystemUser> _signInManager;
        private readonly UserManager<SystemUser> _userManager;

        // Injecao automatico dos serviços do Identity
        public TechnicianController(ITechnicianRepository repository,
                                    UserManager<SystemUser> userManager,
                                    SignInManager<SystemUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /*----------------------------------------------------------------
         * Metodo para popular o Index.cshtml de Technician
         ----------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var technicians = await _repository.GetAllAsync();
            var viewModels = technicians.Select(t => t.ToViewModel()).ToList();
            return View(viewModels);
        }

        /*----------------------------------------------------------------
         * Metodo para display da tela de CreateTechnician
         ----------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> CreateTechnician()
        {
            var model = new TechnicianViewModel
            {
                AvailableUsers = await _repository.GetAvailableUsersAsync()
            };

            return View(model);
        }

        /*----------------------------------------------------------------
         * Metodo para Salvar o CreateTechnician
         ----------------------------------------------------------------*/
        [HttpPost]
        public async Task<IActionResult> CreateTechnician(TechnicianViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var technician = model.ToEntity();
            await _repository.AddAsync(technician);

            return RedirectToAction("Index");
        }

        /*----------------------------------------------------------------
         * Metodo para Display da tela de EditTechnician
         ----------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> EditTechnician(int id)
        {
            var technician = await _repository.GetAsync(id);

            if (technician == null)
            {
                return NotFound();
            }

            var model = technician.ToViewModel();
            return View(model);
        }

        /*----------------------------------------------------------------
         * Metodo para Save EditTechnician 
         ----------------------------------------------------------------*/
        [HttpPost]
        public async Task<IActionResult> EditTechnician(TechnicianViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _repository.UpdateAsync(model.ToEntity());

            return RedirectToAction("Index");
        }

        /*----------------------------------------------------------------
         * Metodo para display da tela de CreateTechnician by JavaScript
         ----------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> GetUSer(string id)
        {
            var techModel = await _repository.GetAsync(id);

            if (techModel == null)
            {
                return NotFound();
            }

            return Json(techModel);
        }












    }
}
