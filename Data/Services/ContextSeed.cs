using Microsoft.AspNetCore.Identity;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Services
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Define as roles padrão do sistema
            string[] roles = { "Admin", "Manager", "User" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedAdminAsync(UserManager<SystemUser> userManager)
        {
            // Dados do administrador padrão
            var defaultUser = new SystemUser
            {
                UserName = "admin@empresa.com",
                Email = "admin@empresa.com",
                FirstName = "Admin",
                LastName = "Sistema",
                Enabled = true,
                UserRole = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            // Verifica se o usuário já existe para não duplicar
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                // ATENÇÃO: Defina uma senha forte aqui em conformidade com as regras do Identity
                var result = await userManager.CreateAsync(defaultUser, "Admin@123456");

                if (result.Succeeded)
                {
                    // Vincula o usuário criado à role "Admin"
                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                }
            }
        }
    }
}
