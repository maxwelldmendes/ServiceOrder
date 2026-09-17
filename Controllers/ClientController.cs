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
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        /*----------------------------------------------------------------
         * Metodo para popular o Index.cshtml de Clientes
         ----------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clientModel = await _context.Client
                                .Include(c => c.CompanyAddress)
                                .Include(c => c.MailAddress)
                                .ToListAsync();

            List<ClientViewModel> clientVM = new List<ClientViewModel>();

            foreach (Client client in clientModel)
            {
                clientVM.Add(client.ToViewModel());
            }
            return View(clientVM);
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

        /*----------------------------------------------------------------------------------------
         * Metodo que trata o click do botao Save Client da tela de criacao
         ---------------------------------------------------------------------------------------*/
        // POST: Client/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClient(ClientViewModel viewModel)
        {
            // Valida se as propriedades obrigatórias dos endereços foram preenchidas
            if (viewModel.CompanyAddress == null || viewModel.MailAddress == null)
            {
                ModelState.AddModelError(string.Empty, "Os dados de endereço comercial e residencial são obrigatórios.");
            }

            ModelState.Remove("CompanyAddress.Street2");
            ModelState.Remove("MailAddress.Street2");

            if (ModelState.IsValid)
            {
                // 2. Mapeia o objeto Cliente conectando as instâncias de endereço criadas acima
                var client = new Client();
                client = viewModel.ToModel();

                // 3. Salva no banco de dados. O EF Core cria automaticamente os endereços primeiro 
                // e amarra os IDs gerados ao novo Cliente graças ao mapeamento de objetos.
                _context.Add(client);
                await _context.SaveChangesAsync();

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
        public async Task<IActionResult> DetailClient(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Client
                .Include(c => c.CompanyAddress)
                .Include(c => c.MailAddress)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return NotFound();

            ClientViewModel clientVM = client.ToViewModel();

            return View(clientVM);
        }

        /*-----------------------------------------------------------------
         * Metodo para abrir a ClientViewodel ao clicar no botao New Client
         ----------------------------------------------------------------*/
        // GET: Clients/Create
        [HttpGet]
        public async Task<IActionResult> EditClient(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Client
                .Include(c => c.CompanyAddress)
                .Include(c => c.MailAddress)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return NotFound();

            ClientViewModel clientVM = client.ToViewModel();

            return View(clientVM);
        }

        /*----------------------------------------------------------------------------------------
         * Metodo que trata o click do botao Save Client da tela de edicao
         ---------------------------------------------------------------------------------------*/
        // POST: Client/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(ClientViewModel viewModel)
        {
            // Valida se as propriedades obrigatórias dos endereços foram preenchidas
            if (viewModel.CompanyAddress == null || viewModel.MailAddress == null)
            {
                ModelState.AddModelError(string.Empty, "Os dados de endereço comercial e residencial são obrigatórios.");
            }

            ModelState.Remove("CompanyAddress.Street2");
            ModelState.Remove("MailAddress.Street2");

            if (ModelState.IsValid)
            {
                // 2. Mapeia o objeto Cliente conectando as instâncias de endereço criadas acima
                var client = new Client();
                client = viewModel.ToModel();

                // 3. Salva no banco de dados. O EF Core cria automaticamente os endereços primeiro 
                // e amarra os IDs gerados ao novo Cliente graças ao mapeamento de objetos.
                _context.Update(client);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Client inserted in database!";
                return RedirectToAction(nameof(Index));
            }

            // Se o modelo for inválido, retorna a View com as validações disparadas
            return View(viewModel);
        }

        /*--------------------------------------------------------------------
         * Metodo para abrir a ClientViewodel ao clicar no botao Delete Client
         -------------------------------------------------------------------*/
        // GET: Clients/Delete
        [HttpGet]
        public async Task<IActionResult> DeleteClient(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Client
                .Include(c => c.CompanyAddress)
                .Include(c => c.MailAddress)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return NotFound();

            ClientViewModel clientVM = client.ToViewModel();

            return View(clientVM);
        }

        /*--------------------------------------------------------------------
         * Metodo para tratar o click do botao Are you sure do Delete Client
         -------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClientById(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Client
                .Include(c => c.CompanyAddress)
                .Include(c => c.MailAddress)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return NotFound();

            // 3. Salva no banco de dados. O EF Core cria automaticamente os endereços primeiro 
            // e amarra os IDs gerados ao novo Cliente graças ao mapeamento de objetos.
            _context.Remove(client);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Client inserted in database!";
            return RedirectToAction(nameof(Index));
        }  
    }
}