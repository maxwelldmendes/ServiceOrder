using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceOrderManager.Data;
using ServiceOrderManager.Data.Services;
using ServiceOrderManager.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar a Connection String para o SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// 2. Configurar os Serviços do Identity
builder.Services
    .AddIdentity<SystemUser, IdentityRole>(options =>
    {
        // Configurações de Senha
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;

        // Configurações de Bloqueio de Conta
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;

        // Configurações de Usuário
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Caminho para onde o usuário é redirecionado se não estiver logado
    options.AccessDeniedPath = "/Account/AccessDenied"; // Acesso negado
    options.ExpireTimeSpan = TimeSpan.FromDays(7); // Tempo de vida do cookie
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// ADICIONE ESTE BLOCO LOGO ANTES DO app.Run()
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Garanta que os tipos aqui coincidam com o que foi registrado no builder.Services
        var userManager = services.GetRequiredService<UserManager<SystemUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Executa a alimentação inicial do banco
        await ContextSeed.SeedRolesAsync(roleManager);
        await ContextSeed.SeedAdminAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao alimentar o banco de dados.");
    }
}
app.Run();
