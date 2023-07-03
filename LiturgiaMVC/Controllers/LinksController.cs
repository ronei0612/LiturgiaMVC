using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class LinksController : Controller
    {
        public IActionResult Index()
        {
            var linksModel = new LinksModel
            {
                LinksDict = Ferramentas.LerArquivoLinks()
            };

            return View(linksModel);
        }

        
        public IActionResult Salvar(string dados)
        {
            string[] linhas = dados.Split(',');

            var linksModel = new LinksModel();
            var dict = new Dictionary<string, string>();
            var list = new List<string>();

            for (int i = 0; i < linhas.Length; i += 2)
            {
                string imagem = linhas[i].Replace(" <div contenteditable=\"\">", "").Replace("</div>", "");
                string link = linhas[i + 1].Replace(" <div contenteditable=\"\">", "").Replace("</div>", "");

                if (imagem.Contains("http") && link.Contains("http"))
                {
                    list.Add(imagem + " = " + link);
                    dict.Add(imagem, link);
                }
            }

            Ferramentas.EscreverArquivoLinks(string.Join(Environment.NewLine, list));

            linksModel.LinksDict = dict;

            return RedirectToAction("Index", "Home", linksModel);
        }
    }
}
