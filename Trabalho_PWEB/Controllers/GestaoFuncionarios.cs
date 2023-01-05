using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trabalho_PWEB.Data;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Controllers
{
    [Authorize(Roles = "Gestor")]

    public class GestaoFuncionarios : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GestaoFuncionarios(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: GestaoFuncionarios
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name;
            var empresaId = _context.Users.Where(u => u.UserName == userId).Select(u => u.EmpresaId).FirstOrDefault();
            if (empresaId == null)
            {
                return View();
            }
            var id = _context.Empresa.Where(e => e.Id == empresaId).Select(e => e.Id).First();
            List<ApplicationUser> u = _context.Users.Where(u => u.EmpresaId == id).ToList();
            List<string> r = new List<string>();
            foreach (var users in u)
            {
                IEnumerable<string> Roles = await _userManager.GetRolesAsync(_userManager.Users.Where(user => user.Id == users.Id).First());
                string aux = "";
                foreach (var item in Roles)
                {
                    aux += item + "; ";
                }
                r.Add(aux);
            }
            ViewBag.r = r;
            ViewBag.id = id;
            string? s = User.Identity.Name;
            ViewBag.uID = _context.Users.Where(u => u.UserName == s).Select(u => u.Id).First();
            return View(u);
        }

        // GET: GestaoFuncionarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GestaoFuncionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GestaoFuncionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateGestaoFuncionariosModelView model, int id)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                EmpresaId = id,
                Ativado = true
            };
            var user = await _userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(defaultUser, model.Password);
                if (model.Gestor == true){
                    await _userManager.AddToRoleAsync(defaultUser, Roles.Gestor.ToString());

                }else{
                    await _userManager.AddToRoleAsync(defaultUser, Roles.Funcionario.ToString());
                }
                return RedirectToAction(nameof(Index));
            }else{
                return View();
            }
        }
        // GET: GestaoFuncionarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GestaoFuncionarios/Edit/5
        public ActionResult Ativo(string id)
        {
            ApplicationUser u = _userManager.Users.Where(u => u.Id == id).First();
            if (u.Ativado == false)
            {
                u.Ativado = true;
            }
            else
            {
                u.Ativado = false;
            }
            _context.Update(u);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: GestaoFuncionarios/Delete/5
        public ActionResult Delete(string id)
        {
            ApplicationUser u = _userManager.Users.Where(u => u.Id == id).First();
            _context.Remove(u);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
