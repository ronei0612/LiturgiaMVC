using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class OrgaoController : Controller
    {
        public IActionResult Index(string tom = "C")
        {
            Ferramentas.EscreverInfoCliente(HttpContext);

            if (Variaveis.acordesLinks == null)
                Ferramentas.LerArquivoAcordesLinks();

            var linksModel = new LinksModel
            {
                Acordes = Variaveis.acordes[tom],
                LinksDict = Variaveis.acordesLinks,
                TomIndex = Array.IndexOf(Variaveis.tonsMaiores, tom),
                TonsMaiores = Variaveis.tonsMaiores,
                TonsMenores = Variaveis.tonsMenores
            };

            return View(linksModel);
        }
    }
}
