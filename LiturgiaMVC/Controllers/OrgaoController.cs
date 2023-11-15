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
                try {
                    Ferramentas.LerArquivoAcordesLinks();
                }
                catch (Exception ex) {
                    return View("Error", new ErrorViewModel {                    
                        Titulo = "Ler Arquivo dos links dos acordes",
                        Mensagem = ex.Message
                    });
                }

            if (Variaveis.tonsMaiores == null)
                try {
                    Ferramentas.LerArquivoAcordesLista();
                }
                catch (Exception ex) {
                    return View("Error", new ErrorViewModel {                    
                        Titulo = "Ler Arquivo da lista de acordes",
                        Mensagem = ex.Message
                    });
                }

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
