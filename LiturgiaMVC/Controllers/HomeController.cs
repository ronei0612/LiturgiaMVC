using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LiturgiaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var textoLinks = Ferramentas.LerArquivoLinks();

            var links = new LinksModel();
            
            links.Adoracao = "https://youtube.com/channel/UC43UPD79xr7t_Vx3HHd3gSw/live";

            foreach (var linha in textoLinks)
            {
                switch (linha.ToLower())
                {
                    case string a when a.Contains("adoracao="):
                        links.Adoracao = linha.ToLower().Replace("adoracao=", "");
                        break;
                    case string a when a.Contains("adoracaoimg="):
                        links.AdoracaoImg = linha.ToLower().Replace("adoracaoimg=", "");
                        break;

                    case string a when a.Contains("aparecia="):
                        links.Aparecia = linha.ToLower().Replace("aparecia=", "");
                        break;
                    case string a when a.Contains("apareciaimg="):
                        links.ApareciaImg = linha.ToLower().Replace("apareciaimg=", "");
                        break;

                    case string a when a.Contains("cancaonova="):
                        links.CancaoNova = linha.ToLower().Replace("cancaonova=", "");
                        break;
                    case string a when a.Contains("cancaonovaimg="):
                        links.CancaoNovaImg = linha.ToLower().Replace("cancaonovaimg=", "");
                        break;

                    case string a when a.Contains("hesed="):
                        links.Hesed = linha.ToLower().Replace("hesed=", "");
                        break;
                    case string a when a.Contains("hesedimg="):
                        links.HesedImg = linha.ToLower().Replace("hesedimg=", "");
                        break;

                    case string a when a.Contains("livros="):
                        links.Livros = linha.ToLower().Replace("livros=", "");
                        break;
                    case string a when a.Contains("livrosimg="):
                        links.LivrosImg = linha.ToLower().Replace("livrosimg=", "");
                        break;

                    case string a when a.Contains("missa="):
                        links.Missa = linha.ToLower().Replace("missa=", "");
                        break;
                    case string a when a.Contains("missaimg="):
                        links.MissaImg = linha.ToLower().Replace("missaimg=", "");
                        break;

                    case string a when a.Contains("rosario="):
                        links.Rosario = linha.ToLower().Replace("rosario=", "");
                        break;
                    case string a when a.Contains("rosarioimg="):
                        links.RosarioImg = linha.ToLower().Replace("rosarioimg=", "");
                        break;

                    case string a when a.Contains("saopatricio="):
                        links.SaoPatricio = linha.ToLower().Replace("saopatricio=", "");
                        break;
                    case string a when a.Contains("saopatricioimg="):
                        links.SaoPatricioImg = linha.ToLower().Replace("saopatricioimg=", "");
                        break;

                    case string a when a.Contains("saopatricio="):
                        links.FreiGilson = linha.ToLower().Replace("saopatricio=", "");
                        break;
                    case string a when a.Contains("saopatricioimg="):
                        links.FreiGilsonImg = linha.ToLower().Replace("saopatricioimg=", "");
                        break;

                    case string a when a.Contains("saopatricio="):
                        links.LiturgiaDiaria = linha.ToLower().Replace("saopatricio=", "");
                        break;
                    case string a when a.Contains("saopatricioimg="):
                        links.LiturgiaDiariaImg = linha.ToLower().Replace("saopatricioimg=", "");
                        break;

                    case string a when a.Contains("saopatricio="):
                        links.LiturgiaDasHoras = linha.ToLower().Replace("saopatricio=", "");
                        break;
                    case string a when a.Contains("saopatricioimg="):
                        links.LiturgiaDasHorasImg = linha.ToLower().Replace("saopatricioimg=", "");
                        break;
                }
            }
            

            links.AdoracaoImg = "https://i.ytimg.com/vi/Ur4XlO5NBvE/maxresdefault.jpg";

            links.Missa = "https://dia.portalodia.com/media/uploads/materias/2020/04/ao-vivo-acompanhe-a-santa-missa-desta-quarta-feira,-1o-de-abril.jpg";

            System.IO.File.WriteAllText("aa.txt", "aa");

            return View(links);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}