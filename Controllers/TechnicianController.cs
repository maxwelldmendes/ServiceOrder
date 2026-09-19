using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var technicianModel = await _context.Technician
                                                .ToListAsync();

            List<TechnicianViewModel> technicianVM = new List<TechnicianViewModel>();

            foreach (Technician techs in technicianModel)
            {
                technicianVM.Add(techs.ToViewModel());
            }
            return View(technicianVM);
        }

        /*--------------------------------------------------------------------------
         * Metodo para abrir a TechnicianViewodel ao clicar no botao New Technician
         -------------------------------------------------------------------------*/
        // GET: Technician/Create
        [HttpGet]
        public IActionResult CreateTechnician()
        {
            // Inicializa o ViewModel com os objetos de endereço instanciados
            var model = new TechnicianViewModel();

            return View(model);
        }
    }
}