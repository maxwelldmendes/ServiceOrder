using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.Models.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the client name.")]
        [StringLength(100, ErrorMessage = "The name must have a maximum of 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "EINumber")]
        [StringLength(10, ErrorMessage = "The EINumber must have a maximum of 10 characters.")]
        public string EINumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10, ErrorMessage = "The Phone Number must have a maximum of 10 characters.")]
        [Phone(ErrorMessage = "Invalid format!")]
        public string PrimaryPhone { get; set; } = string.Empty;

        [Required]
        [Display(Name = "E-mail")]
        [StringLength(100, ErrorMessage = "The Phone Number must have a maximum of 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid E-mail.")]
        public string PrimaryEmail { get; set; } = string.Empty;

        public int CompanyAddressId { get; set; }
        public int MailAddressId { get; set; }

        // Propriedades para exibir os detalhes dos endereços na View/API
        public AddressViewModel? CompanyAddress { get; set; }
        public AddressViewModel? MailAddress { get; set; }
    }
}
