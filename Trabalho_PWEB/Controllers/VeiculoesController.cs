using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_PWEB.Data;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Controllers
{
    [Authorize(Roles = "Admin,Gestor,Funcionario")]
    public class VeiculoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeiculoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veiculoes
        public async Task<IActionResult> Index(int? opcao,int cat)
        {
            if(opcao == null)
            {
                ViewBag.c = _context.Categoria.ToList();
                var userId = User.Identity.Name;
                var empresaId = _context.Users.Where(u => u.UserName == userId).Select(u => u.EmpresaId).FirstOrDefault();
                if (empresaId == null)
                {
                    return View(await _context.Veiculo.ToListAsync());
                }
                var id = _context.Empresa.Where(e => e.Id == empresaId).Select(e => e.Id).First();
                List<Veiculo> v = await _context.Veiculo.Where(v => v.EmpresaId == id).ToListAsync();
                foreach (var veiculo in v)
                {
                    veiculo.Categoria = _context.Categoria.Where(c => c.Id == veiculo.CategoriaID).FirstOrDefault();
                }
                return View(v);
            }else{
                ViewBag.c = _context.Categoria.ToList();
                var userId = User.Identity.Name;
                var empresaId = _context.Users.Where(u => u.UserName == userId).Select(u => u.EmpresaId).FirstOrDefault();
                if (empresaId == null)
                {
                    return View(await _context.Veiculo.ToListAsync());
                }
                var id = _context.Empresa.Where(e => e.Id == empresaId).Select(e => e.Id).First();
                List<Veiculo> v = await _context.Veiculo.Where(v => v.EmpresaId == id).Where(v => v.CategoriaID==cat).ToListAsync();
                foreach (var veiculo in v)
                {
                    veiculo.Categoria = _context.Categoria.Where(c => c.Id == veiculo.CategoriaID).FirstOrDefault();
                }
                
                return View(v);
            }
            
        }

        // GET: Veiculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }
            veiculo.Categoria = _context.Categoria.Where(c => c.Id == veiculo.CategoriaID).FirstOrDefault();
            return View(veiculo);
        }

        // GET: Veiculoes/Create
        public IActionResult Create()
        {
            List<string> c = _context.Categoria.Select(c => c.NomeCategoria).ToList();
            List<int> i = _context.Categoria.Select(c => c.Id).ToList();
            var model = new CreateVeiculoModelView();
            model.Categorias = c;
            model.CategoriasID = i;
            return View(model);
        }

        // POST: Veiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVeiculoModelView model,int cat)
        {
            try
            {
                Veiculo v = new Veiculo();
                v.Matricula = model.Matricula;
                v.Categoria = _context.Categoria.Where(c => c.Id == cat).FirstOrDefault();
                v.CategoriaID = v.Categoria.Id;
                v.Estado = model.Estado;
                var userId = User.Identity.Name;
                var empresaId = _context.Users.Where(u => u.UserName == userId).Select(u => u.EmpresaId).FirstOrDefault();
                if (empresaId == null)
                {
                    List<string> c = _context.Categoria.Select(c => c.NomeCategoria).ToList();
                    List<int> i = _context.Categoria.Select(c => c.Id).ToList();
                    var m = new CreateVeiculoModelView();
                    m.Categorias = c;
                    m.CategoriasID = i;
                    return View(m);
                }
                v.EmpresaId = empresaId;
                v.Preco = model.Preco;
                v.Modelo = model.Modelo;
                v.Marca = model.Marca;
                v.Localização = model.Localizacao;
                v.Ativo = model.Ativo;
                v.Ocupado = false;
                _context.Add(v);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                List<string> c = _context.Categoria.Select(c => c.NomeCategoria).ToList();
                List<int> i = _context.Categoria.Select(c => c.Id).ToList();
                var m = new CreateVeiculoModelView();
                m.Categorias = c;
                m.CategoriasID = i;
                return View(m);
            }
        }

        // GET: Veiculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            List<string> c = _context.Categoria.Select(c => c.NomeCategoria).ToList();
            List<int> i = _context.Categoria.Select(c => c.Id).ToList();
            int selected = _context.Veiculo.Where(v => v.Id == id).Select(v => v.CategoriaID).First();
            ViewBag.c = c;
            ViewBag.i = i;
            ViewBag.selected = selected;
            return View(veiculo);
        }

        // POST: Veiculoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,Modelo,Marca,Estado,Localização,Preco,Ativo")] Veiculo veiculo,int cat)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            try
            {
                Veiculo aux = _context.Veiculo.Where(c => c.Id == veiculo.Id).FirstOrDefault();
                veiculo.DataEntrega = aux.DataEntrega;
                veiculo.EmpresaId = aux.EmpresaId;
                veiculo.Ocupado = aux.Ocupado;
                veiculo.DataEntrega = aux.DataEntrega;
                veiculo.CategoriaID = cat;
                veiculo.Categoria = _context.Categoria.Where(c => c.Id == veiculo.CategoriaID).FirstOrDefault();
                _context.ChangeTracker.Clear();
                _context.Update(veiculo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoExists(veiculo.Id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction(nameof(Edit));

                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Veiculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }
            veiculo.Categoria = _context.Categoria.Where(c => c.Id == veiculo.CategoriaID).FirstOrDefault();

            return View(veiculo);
        }

        // POST: Veiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Veiculo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Veiculo'  is null.");
            }
            var veiculo = await _context.Veiculo.FindAsync(id);
            if (veiculo != null)
            {
                _context.Veiculo.Remove(veiculo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(int id)
        {
          return _context.Veiculo.Any(e => e.Id == id);
        }
    }
}
