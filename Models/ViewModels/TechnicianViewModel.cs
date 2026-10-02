using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.Models.ViewModels
{
    public class TechnicianViewModel
    {
        // Tabela Technician
        public int Id { get; set; }

        [Required(ErrorMessage = "As habilidades do técnico são obrigatórias.")]
        [StringLength(500, ErrorMessage = "As habilidades não podem passar de 500 caracteres.")]
        [Display(Name = "Habilidades")]
        public string Skills { get; set; } = string.Empty;

        [Display(Name = "Ativo")]
        public bool Enabled { get; set; } = true;

        // Tabela SystemUsers

        public string FirstName { get; set; } = string.Empty;
        public string Middlename { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string UserRole { get; set; } = string.Empty;

        //Tebla Technician - Foreign Key para SystemUsers
        [Required(ErrorMessage = "O vínculo com um usuário do sistema é obrigatório.")]
        [Display(Name = "Usuário")]
        public string UserId { get; set; } = string.Empty;

        public List<SelectListItem> AvailableUsers { get; set; } = new();
    }
}
