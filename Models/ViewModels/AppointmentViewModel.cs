using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrderManager.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um cliente.")]
        public int? ClientId { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; }
            = new List<SelectListItem>();

        public string? Title { get; set; }

        public int? AssignedUserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }
    }
}
