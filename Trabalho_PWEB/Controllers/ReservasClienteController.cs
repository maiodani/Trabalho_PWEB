using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trabalho_PWEB.Data;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Controllers
{
    public class ReservasClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasClienteController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ReservasClienteController
        public ActionResult Index()
        {
            ApplicationUser u =_context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            List<Reservas> r = _context.Reservas.Where(r => r.idReservante == u.Id).ToList();
            List<Veiculo> v = new List<Veiculo>();
            List<Empresa> e = new List<Empresa>();
            foreach (var item in r)
            {
                e.Add(_context.Empresa.Where(e => e.Id == (_context.Veiculo.Where(v => v.Id == item.IdVeiculo).Select(v => v.EmpresaId).First())).First());
                v.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
            }
            ListReservasClienteViewModel lrcvm = new ListReservasClienteViewModel();
            lrcvm.v = v;
            lrcvm.r = r;
            lrcvm.e = e;
            return View(lrcvm);
        }

        // GET: ReservasClienteController/Details/5
        public ActionResult Details(int id)
        {
            DetailsReservaClienteViewModel drcvm = new DetailsReservaClienteViewModel();
            drcvm.r = _context.Reservas.Where(r => r.Id == id).First();
            drcvm.v = _context.Veiculo.Where(v => v.Id == drcvm.r.IdVeiculo).First();
            drcvm.e = _context.Empresa.Where(e => e.Id == drcvm.v.EmpresaId).First();
            return View(drcvm);
        }

        // GET: ReservasClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservasClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservasClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservasClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservasClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservasClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
