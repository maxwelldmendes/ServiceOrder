using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceOrderManager.Models.ViewModels
{
    public class SelectionTechnicianViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
    }


}
