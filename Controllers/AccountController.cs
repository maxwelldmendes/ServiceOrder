using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;


namespace ServiceOrderManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<SystemUser> _signInManager;
        private readonly UserManager<SystemUser> _userManager;

        // GET: Account/Register
        [HttpGet]
        [Authorize(Roles = "Admin")] // Bloqueia o acesso para quem não é Admin
        public IActionResult Register()
        {
            // Carrega uma lista estática de perfis para o Select da View
            ViewBag.Roles = new SelectList(new[] { "Admin", "Manager", "Technician", "User" });
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Instancia o SystemUser com os novos campos customizados
                var user = new SystemUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Enabled = true,
                    UserRole = model.UserRole // Guarda a string do perfil no seu campo customizado
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Opcional: Se você usa a infraestrutura de Roles do Identity, 
                    // vincula o usuário à Role oficial também:
                    // await _userManager.AddToRoleAsync(user, model.UserRole);

                    TempData["SuccessMessage"] = "Usuário registrado com sucesso!";
                    return RedirectToAction("Index", "Home");
                }

                // Adiciona os erros retornados pelo Identity (ex: senha fraca, e-mail já existe)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.Roles = new SelectList(new[] { "Admin", "Manager", "Technician", "User",  }, model.UserRole);
            return View(model);
        }

        // O ASP.NET Core injeta automaticamente os serviços do Identity aqui
        public AccountController(SignInManager<SystemUser> signInManager, UserManager<SystemUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl ??= Url.Content("~/"); // Se não houver URL de retorno, vai para a Home

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Realiza o login usando o e-mail (ou username), senha e a opção de persistência de cookie
            // O último parâmetro 'lockoutOnFailure' bloqueia a conta após várias tentativas falhas (opcional)
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas.");
                return View(model);
            }

            // Erro genérico para segurança (não especificar se o erro foi no e-mail ou na senha)
            ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }








        public IActionResult Index()
        {
            return View();
        }
    }
}
