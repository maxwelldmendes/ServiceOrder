using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "The name must be between 2 and 100 characters!")]
        [Display(Name = "Name")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100,
            ErrorMessage = "The middle name must be at maximun 100 characters!")]
        [Display(Name = "MIddle Name")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "The last name is required!")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "c")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The user role is required!")]
        [StringLength(50,
            ErrorMessage = "The user role must be at maximun 50 characters!")]
        [Display(Name = "User Role")]
        public string UserRole { get; set; } = string.Empty;

        [Required(ErrorMessage = "The user name is required!")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "The name name must be between 15 and 50 characters!")]
        [RegularExpression(
            @"^[a-zA-Z0-9._-]+$",
            ErrorMessage = "The username may contain only letters, numbers, periods, hyphens, and underscores!")]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "The eMail is required!")]
        [StringLength(254,
            ErrorMessage = "The eMail must be at maximun 100 characters!")]
        [EmailAddress(ErrorMessage = "Please, type a valid eMail!")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The phone number is required!")]
        [Phone(ErrorMessage = "Type a valid phone number!")]
        [StringLength(20,
            ErrorMessage = "The phone number must be at maximun 20 characters!")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; } = true;
    }
}
