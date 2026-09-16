using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.Models.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a valid Street")]
        [StringLength(150, ErrorMessage = "Street has a minimun lenght")]
        [Display(Name = "Street")]
        public string Street1 { get; set; } = string.Empty;

        [Display(Name = "Street 1")]
        [StringLength(100, ErrorMessage = "Street 2 has a minimun lenght")]
        public string Street2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a valid City.")]
        [StringLength(100, ErrorMessage = "City has a minimun lenght.")]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a valid State.")]
        [StringLength(50, ErrorMessage = "State has a minimun lenght.")]
        [Display(Name = "State")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a valid ZIP code.")]
        [StringLength(20, ErrorMessage = "Zip code has a minimun lenght.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a valid Country")]
        [StringLength(3, ErrorMessage = "Country has a minimun lenght.")]
        [Display(Name = "Country")]
        public string Country { get; set; } = string.Empty;
    }
}
