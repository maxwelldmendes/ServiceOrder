using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceOrderManager.Models.ViewModels
{
    public class UserTechnicianViewModel
    {
        public int Id { get; set; }

        // Armazena o ID selecionado ou enviado no POST do formulário
        public string SelectedUserId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string Middlename { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public bool Enabled { get; set; }

        // Lista de opções para o dropdown (SelectList)
        public SelectList? AvailableUsers { get; set; }
    }
}
