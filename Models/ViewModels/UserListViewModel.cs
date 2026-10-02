using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceOrderManager.Models.ViewModels
{
    public class UserListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public List<SelectListItem> AvailableUsers { get; set; } = new();

    }
}
