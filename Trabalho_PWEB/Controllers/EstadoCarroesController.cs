using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trabalho_PWEB.Data;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Controllers
{
    public class EstadoCarroesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadoCarroesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstadoCarroes
        public async Task<IActionResult> Index()
        {
              return View(await _context.EstadoCarro.ToListAsync());
        }

        // GET: EstadoCarroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EstadoCarro == null)
            {
                return NotFound();
            }

            var estadoCarro = await _context.EstadoCarro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoCarro == null)
            {
                return NotFound();
            }

            return View(estadoCarro);
        }

        // GET: EstadoCarroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoCarroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,idReserva,nKm,DadosCarro,Obs,idReservante")] EstadoCarro estadoCarro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoCarro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoCarro);
        }

        // GET: EstadoCarroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EstadoCarro == null)
            {
                return NotFound();
            }

            var estadoCarro = await _context.EstadoCarro.FindAsync(id);
            if (estadoCarro == null)
            {
                return NotFound();
            }
            return View(estadoCarro);
        }

        // POST: EstadoCarroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,idReserva,nKm,DadosCarro,Obs,idReservante")] EstadoCarro estadoCarro)
        {
            if (id != estadoCarro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoCarro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoCarroExists(estadoCarro.Id))
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
            return View(estadoCarro);
        }

        // GET: EstadoCarroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EstadoCarro == null)
            {
                return NotFound();
            }

            var estadoCarro = await _context.EstadoCarro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoCarro == null)
            {
                return NotFound();
            }

            return View(estadoCarro);
        }

        // POST: EstadoCarroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EstadoCarro == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EstadoCarro'  is null.");
            }
            var estadoCarro = await _context.EstadoCarro.FindAsync(id);
            if (estadoCarro != null)
            {
                _context.EstadoCarro.Remove(estadoCarro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoCarroExists(int id)
        {
          return _context.EstadoCarro.Any(e => e.Id == id);
        }
    }
}
