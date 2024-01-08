﻿using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;

namespace LiturgiaMVC.Controllers
{
    public class OrgaoController : Controller
    {
        public IActionResult Index(string tom = "C")
        {
            var arquivo = @"C:\Users\ronei\Downloads\Entrada-Eis-que-veio-o-Senhor-dos-Senhores.pdf";
            PdfDocument pdf = PdfDocument.FromFile(arquivo);
            string html = pdf.ToHtmlString();
            //site para converter pdf em html https://pdf.io/pt/pdf2html/            

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
                TonsMaioresString = Variaveis.tonsMaioresString,
                TonsMenoresString = Variaveis.tonsMenoresString,
                TonsMenores = Variaveis.tonsMenores,
                RitmosBateria = Variaveis.textoRitmos,
                NotasAcordes = Variaveis.textoNotasAcordes,
                AcidentesCorrespondentes = Ferramentas.ConvertToJson(Variaveis.acidentesCorrespondentes)
            };

            return View(linksModel);
        }

        private IEnumerable<string> ExtractText(CObject cObject)
        {
            var textList = new List<string>();
            if (cObject is COperator)
            {
                var cOperator = cObject as COperator;
                if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
                    cOperator.OpCode.Name == OpCodeName.TJ.ToString())
                {
                    foreach (var cOperand in cOperator.Operands)
                    {
                        textList.AddRange(ExtractText(cOperand));
                    }
                }
            }
            else if (cObject is CSequence)
            {
                var cSequence = cObject as CSequence;
                foreach (var element in cSequence)
                {
                    textList.AddRange(ExtractText(element));
                }
            }
            else if (cObject is CString)
            {
                var cString = cObject as CString;
                textList.Add(cString.Value);
            }
            return textList;
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
            try
            {
                var paginaHtml = string.Empty;

                using (var webClient = new WebClient())
                {
                    paginaHtml = webClient.DownloadString(url);
                }

                var tom = Regex.Split(paginaHtml, "title=\"alterar o tom.*da cifra\">")[1].Split('<')[0].Trim();

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

                cifraTexto = "<head><style>.cifraSelecionada{background-color:#ff0}pre{line-height:1.6;font-size:16px}</style></head>" + textoFinal;

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
    }
}
