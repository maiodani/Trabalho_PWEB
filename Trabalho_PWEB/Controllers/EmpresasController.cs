using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trabalho_PWEB.Data;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public EmpresasController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Empresas
        public async Task<IActionResult> Index(int? opcao)
        {
            
            if (opcao == null) {
                return View(await _context.Empresa.ToListAsync());
            }else{
                switch (opcao)
                {
                    case 1:
                        return View(_context.Empresa.Where(e => e.EstadoSubscricao == false).ToList());
                    case 2:
                        return View(_context.Empresa.Where(e => e.EstadoSubscricao == true).ToList());
                    default:
                        return View();
                        break;    
                }
            }
            return View();
        }

        public async Task<IActionResult> Search(string? nome)
        {
            if(nome == null || nome == ""){
                return View("Index",await _context.Empresa.ToListAsync());
            }
            return View("Index", _context.Empresa.Where(e => e.Nome.Contains(nome)).ToList());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empresa == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }
            List<ApplicationUser> l = new List<ApplicationUser>();
            l = _context.Users.Where(u => u.EmpresaId == id).ToList();
            List<string> rolesUsers = new List<string>();
            foreach (var user in l)
            {
                IEnumerable<string> Roles = await _userManager.GetRolesAsync(_userManager.Users.Where(u => u.Id == user.Id).First());
                string aux = "";
                foreach(var item in Roles)
                {
                    aux += item + "; ";
                }
                rolesUsers.Add(aux);
            }
            ViewBag.rolesUsers = rolesUsers;
            List<Veiculo> v = new List<Veiculo>();
            v = _context.Veiculo.Where(v => v.EmpresaId == id).ToList();
            ViewBag.Veiculo = v;
            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmpresaModelView model)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var user = await _userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(defaultUser, model.Password);
                await _userManager.AddToRoleAsync(defaultUser, Roles.Gestor.ToString());
            }else{
                return View();
            }
            Empresa empresa = new Empresa();
            empresa.Nome = model.Nome;
            empresa.Avaliação = 0;
            empresa.EstadoSubscricao = true;
            ICollection<ApplicationUser> l= new List<ApplicationUser>();
            ICollection<Veiculo> v = new List<Veiculo>();
            l.Add(await _userManager.FindByEmailAsync(defaultUser.Email));
            empresa.ListaFuncionarios = l;
            empresa.Veiculos = v;
            _context.Add(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empresa == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,EstadoSubscricao,Avaliação")] Empresa empresa)
        {

            if (id != empresa.Id)
            {
                return NotFound();
            }
            if (ModelState.ErrorCount<=2)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empresa == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empresa == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Empresa'  is null.");
            }
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa != null)
            {
                if(_context.Veiculo.Where(v => v.EmpresaId == id).Count() == 0) {
                _context.Users.RemoveRange(_context.Users.Where(u => u.EmpresaId == id).ToList());
                _context.Empresa.Remove(empresa);
                }else{
                    return Problem("Empresa tem veiculos registados");
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
          return _context.Empresa.Any(e => e.Id == id);
        }
    }
}
