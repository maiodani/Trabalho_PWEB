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
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index(string? msg)
        {
            ViewBag.msg = msg;
            ApplicationUser u = _context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            List<Veiculo> v = _context.Veiculo.Where(v => v.EmpresaId == u.EmpresaId).ToList();
            List<Reservas> r = new List<Reservas>();
            foreach(var item in v)
            {
                r.AddRange(_context.Reservas.Where(r => r.IdVeiculo == item.Id).Where(r =>r.Acabou == false).ToList());
            }
            ListReservasViewModel lrvm = new ListReservasViewModel();
            lrvm.reservas = r;
            lrvm.users = new List<ApplicationUser>();
            lrvm.veiculo = new List<Veiculo>();
            foreach(var item in r)
            {
                lrvm.users.Add(_context.Users.Where(u => u.Id == item.idReservante).First());
                lrvm.veiculo.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
            }
            return View(lrvm);
        }
        public async Task<IActionResult> Historico()
        {
            ApplicationUser u = _context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            List<Veiculo> v = _context.Veiculo.Where(v => v.EmpresaId == u.EmpresaId).ToList();
            List<Reservas> r = new List<Reservas>();
            foreach (var item in v)
            {
                r.AddRange(_context.Reservas.Where(r => r.IdVeiculo == item.Id).Where(r => r.Acabou == true).ToList());
            }
            ListReservasViewModel lrvm = new ListReservasViewModel();
            lrvm.reservas = r;
            lrvm.users = new List<ApplicationUser>();
            lrvm.veiculo = new List<Veiculo>();
            foreach (var item in r)
            {
                lrvm.users.Add(_context.Users.Where(u => u.Id == item.idReservante).First());
                lrvm.veiculo.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
            }
            return View(lrvm);
        }

        public IActionResult Aceitar(int id)
        {
            Reservas r = _context.Reservas.Where(r => r.Id == id).First();
            r.ReservaAceite = true;
            
            _context.Update(r);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Rejeitar(int id)
        {
            Reservas r = _context.Reservas.Where(r => r.Id == id).First();
            _context.Remove(r);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Receber(int id)
        {
            Reservas r = _context.Reservas.Where(r => r.Id == id).First();
            DateTime now = DateTime.Now;
            if (DateOnly.FromDateTime(r.DataLevantamento).CompareTo(DateOnly.FromDateTime(now)) >= 0)
            {
                ViewBag.id = id;
                return View(_context.EstadoCarro.Where(e => e.idReserva == id).First());
            }
            else
            {
                string msg = "Só pode receber o veiculo depois ou no Dia de Levantamento";
                return RedirectToAction("Index", new { msg });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receber([Bind("idReserva,nKm,Obs,DanosCarro")] EstadoCarro ec)
        {
            EstadoCarro aux = _context.EstadoCarro.Where(e => e.idReserva==ec.idReserva).First();
            ApplicationUser user = _context.Users.Where(u => u.Email == User.Identity.Name).First();
            aux.funcionario = user;
            aux.idFuncionario = user.Id;
            aux.reserva = _context.Reservas.Where(r => r.Id == aux.idReserva).First();
            aux.nKm = ec.nKm;
            aux.Obs = ec.Obs;
            aux.DanosCarro = ec.DanosCarro;
            aux.reserva.Ativa = false;
            aux.reserva.Acabou = true;
            aux.reserva.Veiculo = _context.Veiculo.Where(v => v.Id == aux.reserva.IdVeiculo).First();
            aux.reserva.Veiculo.Ocupado = false;
            _context.Update(aux);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Entregar(int id)
        {
            Reservas r = _context.Reservas.Where(r => r.Id == id).First();
            DateTime now = DateTime.Now;
            if (DateOnly.FromDateTime(r.DataLevantamento).CompareTo(DateOnly.FromDateTime(now)) == 0){
                ViewBag.id = id;
                return View();
            }else{
                string msg = "Só pode entregar o veiculo na Data de Levantamento";
                return RedirectToAction("Index", new { msg });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entregar([Bind("idReserva,nKm,Obs,DanosCarro")] EstadoCarro ec)
        {
            ApplicationUser user = _context.Users.Where(u => u.Email == User.Identity.Name).First();
            ec.funcionario = user;
            ec.idFuncionario = user.Id;
            ec.reserva = _context.Reservas.Where(r => r.Id == ec.idReserva).First();
            ec.reserva.Ativa = true;
            ec.reserva.Veiculo = _context.Veiculo.Where(v => v.Id == ec.reserva.IdVeiculo).First();
            ec.reserva.Veiculo.Ocupado = true;
            _context.Update(ec);
            _context.SaveChanges();
            ec.reserva.idReservaEstadoCarro = _context.EstadoCarro.Where(e => e.idReserva==ec.idReserva).Select(e => e.Id).First();
            _context.Update(ec);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            DetailsReservaFinalViewModel drcvm = new DetailsReservaFinalViewModel();
            drcvm.r = _context.Reservas.Where(r => r.Id == id).First();
            drcvm.v = _context.Veiculo.Where(v => v.Id == drcvm.r.IdVeiculo).First();
            drcvm.e = _context.Empresa.Where(e => e.Id == drcvm.v.EmpresaId).First();
            drcvm.ec = _context.EstadoCarro.Where(ec => ec.idReserva == id).First();
            return View(drcvm);
        }
        private bool ReservasExists(int id)
        {
          return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
