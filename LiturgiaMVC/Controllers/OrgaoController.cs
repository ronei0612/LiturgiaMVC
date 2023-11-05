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
                Acordes = Ferramentas.acordes[tom],
                LinksDict = Ferramentas.acordesLinks,
                TomIndex = Array.IndexOf(Variaveis.tonsMaiores, tom),
                TonsMaiores = Variaveis.tonsMaiores,
                TonsMenores = Variaveis.tonsMenores
            };

            return View(linksModel);
        }
    }
}
