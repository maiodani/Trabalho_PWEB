using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_PWEB.Models;
namespace Trabalho_PWEB.Models
{
    public class UserManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManagerController(UserManager<ApplicationUser> userManager,
       RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserRolesViewModel> userRolesViewModel = new List<UserRolesViewModel>();
            foreach (var user in users)
            {
                var aux=new UserRolesViewModel();
                aux.UserId = user.Id;
                aux.UserName = user.UserName;
                aux.PrimeiroNome = user.PrimeiroNome;
                aux.UltimoNome = user.UltimoNome;
                IEnumerable<string> Roles = await _userManager.GetRolesAsync(_userManager.Users.Where(u => u.Id == user.Id).First());
                aux.Roles = Roles;
                userRolesViewModel.Add(aux);
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        public async Task<IActionResult> Details(string userId)
        {

            if (userId == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var aux = new UserRolesViewModel();
            aux.UserId = user.Id;
            aux.UserName = user.UserName;
            aux.PrimeiroNome = user.PrimeiroNome;
            aux.UltimoNome = user.UltimoNome;
            IEnumerable<string> Roles = await _userManager.GetRolesAsync(_userManager.Users.Where(u => u.Id == user.Id).First());
            aux.Roles = Roles;

            return View(aux);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var model = new List<ManageUserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.ToListAsync();

            ViewBag.UserName = user.UserName;
            foreach (var role in roles)
            {
                model.Add(new ManageUserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Selected = await _userManager.IsInRoleAsync(user, role.Name),
                    UserID = userId
                }
                );
            }
            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(List<ManageUserRolesViewModel> model)
        {
            var userId = model[0].UserID;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Index");
                //return View();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Unable to remove existing roles.");
                return View();
            }
            await _userManager.AddToRolesAsync(user,
                model
                .Where(r => r.Selected)
                .Select(r => r.RoleName));

            return RedirectToAction("Index");
        }
        
    }
}

