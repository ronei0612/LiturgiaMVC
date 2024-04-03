using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;

namespace LiturgiaMVC.Controllers
{
    public class OrgaoController : Controller
    {
        public IActionResult Index(string compartilhado = "")
        {
            var tom = "C";
            //verificar arquivo acordeslinks.txt
            //criar arquivo AcordesLinks.txt
            Ferramentas.EscreverInfoCliente(HttpContext);

            var result = Ferramentas.CarregarVariaveis();
            if (result != null)
                return View("Error", result);

            if (compartilhado != "")
				compartilhado = Ferramentas.LerArquivoGoogleDrive(compartilhado);

			var linksModel = new LinksModel
            {
                AcordesCampoHarmonico = Ferramentas.ConvertToJson(Variaveis.acordes),
                Acordes = Variaveis.acordes[tom],
                LinksDict = Variaveis.notasLinks,
                TomIndex = Array.IndexOf(Variaveis.tonsMaiores, tom),
                TonsMaiores = Variaveis.tonsMaiores,
                TonsMaioresString = Variaveis.tonsMaioresString,
                TonsMenoresString = Variaveis.tonsMenoresString,
                TonsMenores = Variaveis.tonsMenores,
                RitmosBateria = Variaveis.textoRitmos,
                NotasAcordes = Variaveis.textoNotasAcordes,
                AcidentesCorrespondentes = Ferramentas.ConvertToJson(Variaveis.acidentesCorrespondentes),
                Compartilhado = compartilhado
			};

            return View(linksModel);
        }

        [HttpPost]
        public JsonResult Pesquisar(string texto)
        {
            /*
             * Este método utiliza API do Google
             * Para criar e gerar a apiKey precisei acessar o site:
             * https://console.cloud.google.com/apis/credentials/key/
             * Sessão Credentials (credenciais)
             * e criar uma API Key (chave de API), na verdade já veio criada com o nome:
             * Browser key (auto created by Firebase)
             * Selecionado Definir uma restrição de aplicativo para NENHUM
             * Restrições da API para NÃO RESTRINGIR A CHAVE
             * 
             * Para criar o searchEngineId foi preciso ativar o serviço Custom Search API em
             * https://programmablesearchengine.google.com/controlpanel/all
             * Utilizei esse site para seguir o passo a passo:
             * https://help.elfsight.com/article/331-how-to-get-search-engine-id
             * O ID do mecanismo de pesquisa é o searchEngineId
             * É possível monitorar em Sessão APIs e Serviços Ativados
             * https://console.cloud.google.com/apis/api/customsearch.googleapis.com/             
             */

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
            try
            {
                var paginaHtml = string.Empty;

                using (var webClient = new WebClient())
                {
                    paginaHtml = webClient.DownloadString(url);
                }

                var tom = Regex.Split(paginaHtml, "title=\"alterar o tom.*da cifra\">")[1].Split('<')[0].Trim();
                var retorno = Ferramentas.GetAcorde(tom);
                tom = retorno[0] + retorno[1];

                paginaHtml = paginaHtml.Split("<pre>")[1].Split("</pre>")[0];
                paginaHtml = "<pre>" + paginaHtml + "</pre>";

                //var linhas = paginaHtml.Split('\n');
                //var list = new List<string>();

                //foreach (var linha in linhas)
                //{
                //    if (!linha.Contains("[Solo]") && !linha.Contains("[Final]"))
                //        list.Add(linha);
                //}

                //var cifraTexto = string.Join('\n', list);
                var cifraTexto = paginaHtml;

                if (cifraTexto.Contains("<pre>") == false)
                    cifraTexto = "<pre>" + cifraTexto;

                if (cifraTexto.Contains("</pre>") == false)
                    cifraTexto += "</pre>";

                var textoFinal = Ferramentas.GetAcordes(cifraTexto);

                cifraTexto = "<head><style>.cifraSelecionada{background-color:#ff0}pre{line-height:1.6;font-size:14px}</style></head>" + textoFinal;

                return Json(new
                {
                    success = true,
                    message = cifraTexto,
                    tom = tom
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult AlterarTom(string texto, bool aumentar, int quant)
        {
            try {
                if (aumentar == false)
                    quant = -1 * quant;
                var cifraTexto = Ferramentas.GetAcordes(texto, quant);

                return Json(new
                {
                    success = true,
                    message = cifraTexto
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult EditarCifra(string texto)
        {
            try
            {
                var cifraTexto = Ferramentas.SearchAcordes(texto);

                return Json(new
                {
                    success = true,
                    message = cifraTexto
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
