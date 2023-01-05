using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trabalho_PWEB.Data;
using Trabalho_PWEB.Models;

namespace Trabalho_PWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
    }

        public IActionResult Index(string? msg) {
            PesquisaReservaModelView prmv = new PesquisaReservaModelView();
            prmv.Categorias = _context.Categoria.ToList();
            prmv.Localizacao = "";
            DateTime date = System.DateTime.Now;
            //prmv.DataLevantamento = DateOnly.Parse(date.ToString("dd/MM/yyyy"));
            //prmv.DataEntrega = new DateOnly();
            ViewBag.msg = msg;
            return View(prmv);
        }
        public async Task<IActionResult> Search([Bind("Localizacao")]PesquisaReservaModelView prmv,string DataLevantamento, string DataEntrega, int cat)
        {
            if (prmv.Localizacao == null ||DataEntrega == null || DataLevantamento == null)return RedirectToAction("Index","Home",new { msg = "Preecha todos os campos!" });
            
            prmv.DataEntrega = new DateOnly();
            prmv.DataEntrega = DateOnly.Parse(DataEntrega);
            prmv.DataLevantamento = new DateOnly();
            prmv.DataLevantamento = DateOnly.Parse(DataLevantamento);
            if (prmv.DataEntrega.CompareTo(prmv.DataLevantamento) <= 0){
                return RedirectToAction("Index", "Home", new { msg = "Data de Levantamento tem que ser antes da Data de Entrega!" });
            }
            if(prmv.DataLevantamento.CompareTo(DateTime.Now) < 0){
            List<Reservas> r = _context.Reservas.Where(r => r.Acabou == false).ToList();
            List<Veiculo> veiculosComReserva = new List<Veiculo>();
            foreach (var item in r){ //VAI ADICIONAR A LISTA TODAS AS RESERVAS QUE ESTAO NA TIME FRAME QUE O UTLIZADOR INTRODUZIU
                if (DateOnly.FromDateTime(item.DataLevantamento).CompareTo(prmv.DataLevantamento) >= 0 && DateOnly.FromDateTime(item.DataEntrega).CompareTo(prmv.DataEntrega) <= 0){
                    veiculosComReserva.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
                    System.Diagnostics.Debug.WriteLine("ESTA DENTRO DA TIME FRAME!!!");
                }else{
                    if (DateOnly.FromDateTime(item.DataLevantamento).CompareTo(prmv.DataLevantamento) >= 0 && DateOnly.FromDateTime(item.DataLevantamento).CompareTo(prmv.DataEntrega) <= 0){
                        veiculosComReserva.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
                        System.Diagnostics.Debug.WriteLine("ESTA DENTRO DA TIME FRAME!!");
                    }else{
                        if (DateOnly.FromDateTime(item.DataEntrega).CompareTo(prmv.DataLevantamento) >= 0 && DateOnly.FromDateTime(item.DataEntrega).CompareTo(prmv.DataEntrega) <= 0){
                            veiculosComReserva.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
                            System.Diagnostics.Debug.WriteLine("ESTA DENTRO DA TIME FRAME!!!");
                        }
                    }
                }
            }
            List<Veiculo> v = new List<Veiculo>();
            if (cat == 0)
            {
                v = _context.Veiculo.Where(v => v.Ocupado == false).Where(v => v.Ativo == true).Where(v => v.Localização == prmv.Localizacao).ToList();
            }else{
                v = _context.Veiculo.Where(v => v.Ocupado == false).Where(v => v.Ativo == true).Where(v => v.Localização == prmv.Localizacao).Where(v => v.CategoriaID == cat).ToList();
            }
            foreach (var item in veiculosComReserva){
                if (v.Contains(item)) v.Remove(item);
            }
            foreach(var item in v)
            {
                item.Categoria = _context.Categoria.Where(c => c.Id == item.CategoriaID).First();
            }
            
            ViewBag.c = _context.Categoria.ToList();
            ListVeiculosViewModel lvvm = new ListVeiculosViewModel();
            lvvm.empresa = new List<Empresa>();
            foreach (var item in v)
            {
                lvvm.empresa.Add(_context.Empresa.Where(e => e.Id == item.EmpresaId).First());
            }
            lvvm.veiculos = v;
            lvvm.DataEntrega = prmv.DataEntrega.ToString();
            lvvm.DataLevantamento = prmv.DataLevantamento.ToString();
            lvvm.Localizacao = prmv.Localizacao;
            return View("List", lvvm);
        }

        public IActionResult Filtar(int cat, ListVeiculosViewModel lvvm) {
            PesquisaReservaModelView prmv = new PesquisaReservaModelView();
            prmv.Localizacao = lvvm.Localizacao;
            return RedirectToAction("Search", new { prmv.Localizacao, lvvm.DataLevantamento, lvvm.DataEntrega, cat });
        }
        [Authorize(Roles = "Admin,Cliente")]

        public async Task<IActionResult> Reservar(int id,string dataE,string dataL)
        {
            Reservas r = new Reservas();
            r.idReservante = _context.Users.Where(u => u.Email == User.Identity.Name).Select(u => u.Id).First();
            r.IdVeiculo = _context.Veiculo.Where(v => v.Id == id).Select(v => v.Id).First();
            r.Veiculo = _context.Veiculo.Where(v => v.Id == id).First();
            r.DataLevantamento = DateTime.Parse(dataL);
            r.DataEntrega = DateTime.Parse(dataE);
            r.Ativa = false;
            r.ReservaAceite = false;
            r.Acabou = false;
            _context.Add(r);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> List(List<Veiculo> v){
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}