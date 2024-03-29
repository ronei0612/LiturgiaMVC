using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace LiturgiaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Ferramentas.EscreverInfoCliente(HttpContext);

            var linksModel = new LinksModel
            {
                LinksDict = Ferramentas.LerArquivoLinks()
            };

            return View(linksModel);
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Confissao()
        {
            return View("Confissao");
        }

        public IActionResult Decalogo()
        {
            return View("Decalogo");
        }

        public IActionResult Catecismo()
        {
            return View("Catecismo");
        }

        public IActionResult Compendio()
        {
            return View("Compendio");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}