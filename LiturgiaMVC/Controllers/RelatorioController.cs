using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            var lines = System.IO.File.ReadAllLines(Variaveis.arquivoIPs);            

            var linksModel = new LinksModel
            {
                RelatorioLines = lines
            };

            return View(linksModel);
        }
    }
}
