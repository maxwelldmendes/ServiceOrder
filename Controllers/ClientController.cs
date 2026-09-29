using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Constants;
using ServiceOrderManager.Mappings;
using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;
using ServiceOrderManager.Repositories;

namespace ServiceOrderManager.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IRepository<Client> _repository;
        private readonly SignInManager<SystemUser> _signInManager;
        private readonly UserManager<SystemUser> _userManager;

        // O ASP.NET Core injeta automaticamente os serviços do Identity aqui
        public ClientController(IRepository<Client> repository,
                                 UserManager<SystemUser> userManager,
                                 SignInManager<SystemUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /*----------------------------------------------------------------
         * Metodo para popular o Index.cshtml de Clientes
         ----------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Client> clients = await _repository.GetAllAsync();

            return View(clients);
        }

        /*-----------------------------------------------------------------
         * Metodo para abrir a ClientViewodel ao clicar no botao New Client
         ----------------------------------------------------------------*/
        // GET: Clients/Create
        [HttpGet]
        public IActionResult CreateClient()
        {
            // Inicializa o ViewModel com os objetos de endereço instanciados
            var model = new ClientViewModel
            {
                CompanyAddress = new AddressViewModel(),
                MailAddress = new AddressViewModel()
            };
            return View(model);
        }

        /*---------------------------------------------------------------------
         * Post: Clientes/Create
         --------------------------------------------------------------------*/
        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientViewModel clienteVm)
        {
            if (!ModelState.IsValid)
            {
                return View(clienteVm);
            }

            Client clientEntity = clienteVm.ToEntity();

            await _repository.AddAsync(clientEntity);

            return RedirectToAction(nameof(Index));
        }

        /*---------------------------------------------------------------------
         * Post: Cliente/EditCliente/id
         --------------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> EditClient(int id)
        {
            Client client = await _repository.GetAsync(id);

            if (client == null)
            {
                return View();
            }

            ClientViewModel clientVm = client.ToViewModel();

            return View(clientVm);
        }

        /*----------------------------------------------------------------------------------------
        * Metodo que trata o click do botao Save Client da tela de edicao
         ---------------------------------------------------------------------------------------*/
        // POST: Client/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(ClientViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                // 2. Mapeia o objeto Cliente conectando as instâncias de endereço criadas acima
                var client = viewModel.ToEntity();

                await _repository.UpdateAsync(client);

                TempData["SuccessMessage"] = "Client inserted in database!";
                return RedirectToAction(nameof(Index));
            }

            // Se o modelo for inválido, retorna a View com as validações disparadas
            return View(viewModel);
        }

        /*-------------------------------------------------------------------------------------------
         * Metodo que trata o click do botao Client Detail na tabela de clientes cadastrados 
         ------------------------------------------------------------------------------------------*/
        // GET: Client/Details/5
        public async Task<IActionResult> DetailClient(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client client = await _repository.GetAsync(id); 

            ClientViewModel clientVM = client.ToViewModel();

            return View(clientVM);
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _repository.GetAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            //var userId = _userManager.GetUserId(User);
            /*
            if (User IsInRoll(Roles.Admin) == false && Client.UserId != userId)
            {
                return Forbid();
            }
            */
            await _repository.DeleteAsync(id);

            return Ok();
        }
    }
}