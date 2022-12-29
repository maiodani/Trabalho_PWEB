using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trabalho_PWEB.Controllers
{
    public class ReservasClienteController : Controller
    {
        // GET: ReservasClienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReservasClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
