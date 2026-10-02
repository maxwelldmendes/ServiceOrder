using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceOrderManager.Models;
using ServiceOrderManager.Models.ViewModels;

namespace ServiceOrderManager.Repositories
{
    public interface ITechnicianRepository : IRepository<Technician>
    {
        Task<List<SelectListItem>> GetAvailableUsersAsync();


        new Task<TechnicianViewModel> GetAsync(string id);
    }
}
