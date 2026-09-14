using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O primeiro nome é obrigatório.")]
        [Display(Name = "Primeiro Nome")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Nome do Meio")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
        [Display(Name = "E-mail (Nome de Usuário)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione o perfil do usuário.")]
        [Display(Name = "Perfil (Role)")]
        public string UserRole { get; set; } = string.Empty;
    }
}
