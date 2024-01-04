using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost]
        public JsonResult Pesquisar(string texto)
        {
            string apiKey = "AIzaSyBWjthtU-mhxX8-YtWh6mLJoiWdLdD9hwE";
            string searchEngineId = "c226ebc5ae2754d20";
            string query = "cifra " + texto;

            try
            {
                var customSearchService = new CustomSearchAPIService(new BaseClientService.Initializer
                {
                    ApiKey = apiKey
                });

                CseResource.ListRequest listRequest = customSearchService.Cse.List();
                listRequest.Cx = searchEngineId;
                listRequest.Q = query;

                var linksReturned = new List<string>();
                var titlesReturned = new List<string>();

                listRequest.Start = 0;
                var linksEncontrados = listRequest.Execute().Items;

                if (linksEncontrados == null)
                    return Json(new
                    {
                        success = false,
                        message = "Música não encontrada em CifraClub.com.br"
                    });

                foreach (var item in linksEncontrados)
                    if (item.Title.Contains("(letra da música)") == false) {
                        titlesReturned.Add(item.Title.Replace(" - Cifra Club", ""));
                        linksReturned.Add(item.Link);
                    }

                return Json(new
                {
                    success = true,
                    lista = titlesReturned.ToArray(),
                    links = linksReturned.ToArray()
                });
            }

            catch (Exception ex)
            {
                return Json(new
                {
                    success = true,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult DownloadSite(string url)
        {
            var paginaHtml = string.Empty;

            using (var webClient = new WebClient())
            {
                paginaHtml = webClient.DownloadString(url);
            }

            paginaHtml = paginaHtml.Split("<pre>")[1].Split("</pre>")[0];
            paginaHtml = "<pre>" + paginaHtml + "</pre>";

            var linhas = paginaHtml.Split('\n');
            var list = new List<string>();

            foreach (var linha in linhas)
            {
                if (!linha.Contains("[Intro]") && !linha.Contains("[Solo]") && !linha.Contains("[Final]"))
                    list.Add(linha);
            }

            var cifra = string.Join('\n', list);

            if (cifra.Contains("<pre>") == false)
                cifra = "<pre>" + cifra;

            if (cifra.Contains("</pre>") == false)
                cifra += "</pre>";

            
            var notas = cifra.Split("<b");
                                             
            var texto = new List<string>();
            for (int i = 0; i < notas.Length; i++)
                if (i == 0)
                    texto.Add(notas[i]);
                else
                    texto.Add("<b id=\"cifra" + i + "\"" + notas[i]);
            //texto.Add("<b id=\"cifra" + i + "\" class=\"cifra\"')\"" + notas[i]);
            //texto.Add("<b id=\"cifra" + i + "\" class=\"cifra\" onclick=\"tocarCifra('cifra" + i + "')\"" + notas[i]);

            //cifra = string.Join("", texto);
            cifra = "<head><style>.cifraSelecionada{background-color:#ff0}</style></head>" + string.Join("", texto);

            return Json(new
            {
                success = true,
                message = cifra
            });
        }
    }
}
