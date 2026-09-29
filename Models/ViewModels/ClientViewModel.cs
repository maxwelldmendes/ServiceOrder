using ServiceOrderManager.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.Models.ViewModels // Ou o namespace que estiver usando para ViewModels
{
    public class ClientViewModel
    {
        // O Id fica oculto/opcional (útil para cenários de Edição, mas ignorado na Criação)
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres.")]
        [Display(Name = "Nome do Cliente")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número de identificação (CNPJ/CPF) é obrigatório.")]
        [Display(Name = "Inscrição Estadual / Documento (EI Number)")]
        public string EINumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone principal é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        [Display(Name = "Telefone Principal")]
        public string PrimaryPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail principal é obrigatório.")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido.")]
        [Display(Name = "E-mail Principal")]
        public string PrimaryEmail { get; set; } = string.Empty;

        // Reaproveita a estrutura que você já instanciou no seu método CreateClient()
        [Display(Name = "Endereço da Empresa")]
        public AddressViewModel CompanyAddress { get; set; } = new AddressViewModel();

        [Display(Name = "Endereço de Correspondência")]
        public AddressViewModel MailAddress { get; set; } = new AddressViewModel();
    }
}
