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
            if (prmv.Localizacao == null || DataEntrega == null || DataLevantamento == null)
            {
                return RedirectToAction("Index","Home",new { msg = "Preecha todos os campos!" });
            }else{

            }
            prmv.DataEntrega = new DateOnly();
            prmv.DataEntrega = DateOnly.Parse(DataEntrega);
            prmv.DataLevantamento = new DateOnly();
            prmv.DataLevantamento = DateOnly.Parse(DataLevantamento);
            
            List<Reservas> r = _context.Reservas.ToList();
            List<Veiculo> veiculosComReserva = new List<Veiculo>();
            foreach (var item in r){ //VAI ADICIONAR A LISTA TODAS AS RESERVAS QUE ESTAO NA TIME FRAME QUE O UTLIZADOR INTRODUZIU
                if (item.DataLevantamento.Date.CompareTo(prmv.DataLevantamento) >= 0 && item.DataEntrega.Date.CompareTo(prmv.DataEntrega) <= 0){
                    veiculosComReserva.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
                    System.Diagnostics.Debug.WriteLine("ESTA DENTRO DA TIME FRAME!!!");
                }else{
                    if (item.DataLevantamento.Date.CompareTo(prmv.DataLevantamento) >= 0 && item.DataLevantamento.Date.CompareTo(prmv.DataEntrega) <= 0){
                        veiculosComReserva.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
                        System.Diagnostics.Debug.WriteLine("ESTA DENTRO DA TIME FRAME!!");
                    }else{
                        if (item.DataEntrega.Date.CompareTo(prmv.DataLevantamento) >= 0 && item.DataEntrega.Date.CompareTo(prmv.DataEntrega) <= 0){
                            veiculosComReserva.Add(_context.Veiculo.Where(v => v.Id == item.IdVeiculo).First());
                            System.Diagnostics.Debug.WriteLine("ESTA DENTRO DA TIME FRAME!!!");
                        }
                    }
                }
            }
            List<Veiculo> v = new List<Veiculo>();
            v = _context.Veiculo.Where(v => v.Ocupado == false).Where(v=> v.Ativo == true).Where(v => v.Localização == prmv.Localizacao).Where(v => v.CategoriaID == cat).ToList();
            foreach(var item in veiculosComReserva){
                if (v.Contains(item)) v.Remove(item);
            }
            foreach(var item in v)
            {
                item.Categoria = _context.Categoria.Where(c => c.Id == item.CategoriaID).First();
            }
            return View("List", v );
        }
        public async Task<IActionResult> List(List<Veiculo> v){
            return View(v);
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