using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trabalho_PWEB.Data;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Controllers
{
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
                System.Diagnostics.Debug.WriteLine();
                return View(await _context.Empresa.ToListAsync());
            }else{
                switch (opcao)
                {
                    case 1:
                        System.Diagnostics.Debug.WriteLine("CASE 1");
                        return View(_context.Empresa.Where(e => e.EstadoSubscricao == false).ToList());
                    case 2:
                        System.Diagnostics.Debug.WriteLine("CASE 2");
                        return View(_context.Empresa.Where(e => e.EstadoSubscricao == true).ToList());
                    case 3:
                        break;
                    default:
                        return View();
                        break;    
                }
            }
            return View();
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
            System.Diagnostics.Debug.WriteLine(model.Nome);
            System.Diagnostics.Debug.WriteLine(model.Email);
            System.Diagnostics.Debug.WriteLine(model.Password);
            System.Diagnostics.Debug.WriteLine(model.ConfirmPassword);
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
            }
            Empresa empresa = new Empresa();
            empresa.Nome = model.Nome;
            empresa.Avaliação = 0;
            empresa.EstadoSubscricao = true;
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

            if (ModelState.IsValid)
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
                _context.Empresa.Remove(empresa);
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
