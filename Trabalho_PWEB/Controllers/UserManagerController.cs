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
            System.Diagnostics.Debug.WriteLine(userRolesViewModel);
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        public async Task<IActionResult> Details(string userId)
        {
            System.Diagnostics.Debug.WriteLine("teste "+userId);

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

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeCategoria")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        */
    }
}

