using Microsoft.AspNetCore.Identity;

namespace ServiceOrderManager.Models
{
    public class SystemUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public string UserRole { get; set; } = string.Empty;

        // Navegação 1:1
        public Technician? Technician { get; set; }
    }
}
