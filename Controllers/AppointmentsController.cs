using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceOrderManager.Data;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<SystemUser> _userManager;

        public AppointmentsController(AppDbContext context, UserManager<SystemUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }
}
