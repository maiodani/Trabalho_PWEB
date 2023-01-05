using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Trabalho_PWEB.Controllers
{
    public class RoleManagerController : Controller
    {
        
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();
            System.Diagnostics.Debug.WriteLine(roles);
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var role = new IdentityRole(roleName);
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
