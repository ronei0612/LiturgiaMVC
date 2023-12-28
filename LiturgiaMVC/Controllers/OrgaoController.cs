using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class OrgaoController : Controller
    {
        public IActionResult Index(string tom = "C")
        {
            //verificar arquivo acordeslinks.txt
            //criar arquivo AcordesLinks.txt
            Ferramentas.EscreverInfoCliente(HttpContext);

            var result = Ferramentas.CarregarVariaveis();
            if (result != null)
                return View("Error", result);

            var linksModel = new LinksModel
            {
                AcordesCampoHarmonico = Ferramentas.ConvertToJson(Variaveis.acordes),
                Acordes = Variaveis.acordes[tom],
                LinksDict = Variaveis.notasLinks,
                TomIndex = Array.IndexOf(Variaveis.tonsMaiores, tom),
                TonsMaiores = Variaveis.tonsMaiores,
                TonsMenores = Variaveis.tonsMenores,
                RitmosBateria = Variaveis.textoRitmos,
                NotasAcordes = Variaveis.textoNotasAcordes,
                AcidentesCorrespondentes = Ferramentas.ConvertToJson(Variaveis.acidentesCorrespondentes)
            };

            return View(linksModel);
        }

        public IActionResult Gravacao()
        {
            return View();
        }
    }
}
