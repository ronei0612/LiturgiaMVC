using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using LiturgiaMVC.Models;

namespace LiturgiaMVC.Controllers
{
    public class LiturgiaController : Controller
    {
        public IActionResult Index()
        {
            var url = "https://www.paulus.com.br/portal/liturgia-diaria/";
            var paginaHtml = string.Empty;

            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                paginaHtml = webClient.DownloadString(url);
            }

            var liturgiaTexto = Regex.Split(Regex.Split(paginaHtml, "id=\"texto\">")[1], "<div class=\"tittleinterno\">")[0];

            var liturgiaModel = new LiturgiaModel()
            {
                TextoHtml = liturgiaTexto,
            };

            return View(liturgiaModel);
        }

        public IActionResult OracoesEucaristicas()
        {
            return View();
        }
    }
}
