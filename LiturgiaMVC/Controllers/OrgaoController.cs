using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class OrgaoController : Controller
    {
        public IActionResult Index(string tom = "C")
        {
            Ferramentas.EscreverInfoCliente(HttpContext);

            var linksModel = new LinksModel
            {
                AcordesLinks = Ferramentas.acordes[tom],
                LinksDict = Ferramentas.acordesLinks,
                NotasAcordes = Ferramentas.escalas,
                NotasLinks = Ferramentas.notasLinks,
                Tom = tom
            };

            return View(linksModel);
        }

        public IActionResult MudarTom(string tom)
        {
            Ferramentas.EscreverInfoCliente(HttpContext);

            var linksModel = new LinksModel
            {
                AcordesLinks = Ferramentas.acordes[tom]
            };

            return View(linksModel);
        }
    }
}
