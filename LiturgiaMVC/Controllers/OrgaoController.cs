using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LiturgiaMVC.Controllers
{
    public class OrgaoController : Controller
    {
        public IActionResult Index(string tom = "C")
        {
            //verificar arquivo acordeslinks.txt
            //criar arquivo AcordesLinks.txt
            Ferramentas.EscreverInfoCliente(HttpContext);

            if (Variaveis.textoNotasAcordes == "")
                try {
                    Ferramentas.LerArquivoNotasAcordes();
                }
                catch (Exception ex) {
                    return View("Error", new ErrorViewModel {                    
                        Titulo = "Ler Arquivo das notas dos acordes",
                        Mensagem = ex.Message
                    });
                }

            if (Variaveis.acordesLinks == null)
                try {
                    Ferramentas.LerArquivoNotasLinks();
                }
                catch (Exception ex) {
                    return View("Error", new ErrorViewModel {                    
                        Titulo = "Ler Arquivo dos links das notas",
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

            if (Variaveis.textoRitmos == "")
                try {
                    Ferramentas.LerArquivoRitmosBateria();
                }
                catch (Exception ex) {
                    return View("Error", new ErrorViewModel {                    
                        Titulo = "Ler Arquivo dos ritmos de bateria",
                        Mensagem = ex.Message
                    });
                }

            var linksModel = new LinksModel
            {
                Acordes = Variaveis.acordes[tom],
                LinksDict = Variaveis.notasLinks,
                TomIndex = Array.IndexOf(Variaveis.tonsMaiores, tom),
                TonsMaiores = Variaveis.tonsMaiores,
                TonsMenores = Variaveis.tonsMenores,
                RitmosBateria = Variaveis.textoRitmos,
                NotasAcordes = Variaveis.textoNotasAcordes
            };

            return View(linksModel);
        }
    }
}
