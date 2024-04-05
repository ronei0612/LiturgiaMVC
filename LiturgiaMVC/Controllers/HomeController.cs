using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

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
            Ferramentas.EscreverInfoCliente(HttpContext);

            var linksModel = new LinksModel
            {
                LinksDict = Ferramentas.LerArquivoLinks()
            };

            return View(linksModel);
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Confissao()
        {
            return View("Confissao");
        }

        public IActionResult Decalogo()
        {
            return View("Decalogo");
        }

        public IActionResult Catecismo()
        {
            return View("Catecismo");
        }

        public IActionResult Compendio()
        {
            return View("Compendio");
        }

        public IActionResult Santos()
        {
            return View("Santos");
        }

        public IActionResult PadrePio()
        {
            return View("PadrePio");
        }

        public IActionResult CarloAcutis()
        {
            return View("CarloAcutis");
        }

        public IActionResult CatecismoSaoPioX()
        {
            return View("CatecismoSaoPioX");
        }

        public IActionResult Tratado()
        {
            return View("Tratado");
        }

        public IActionResult MilagresEucaristicos()
        {
            return View("MilagresEucaristicos");
        }

        public IActionResult Evangelho()
        {
            return View("Evangelho");
        }

        public IActionResult VerficarCertificado()
        {
            var liturgiaModel = new LiturgiaModel
            {
                ValidadeCertificado = Ferramentas.VerificarValidadeCertificado(HttpContext),
                CertificadoVencendo = Variaveis.certificadoVencendo
            };
            
            return View("VerficarCertificado", liturgiaModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult ObterDataValidadeCertificado()
        {
            var url = HttpContext.Request.Host.Value;
            var linhasCertificado = Ferramentas.LerArquivoCertificado();
            var dataHoraBrasilia = DateTime.Now.AddHours(2);
            //var dataHoraAtual = dataHoraBrasilia.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));

            if (linhasCertificado != null) {
                foreach (var linha in linhasCertificado)
                {
                    if (linha.Contains('='))
                    {
                        if (linha.Split('=')[0] == url)
                        {
                            var possivelData = linha.Split('=')[1];
                            if (DateTime.TryParseExact(possivelData, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime dataExpiracao))
                            {
                                // Verifique se faltam 20 dias para a expiração do certificado
                                if (dataExpiracao.Subtract(dataHoraBrasilia).Days <= 20)
                                    return Json(new { success = false });
                            }
                        }
                    }
                }
            }

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public string AtualizarDataValidadeCertificado(string dataValidade, string senha)
        {
            if (senha == Variaveis.Senha)
            {
                if (DateTime.TryParseExact(dataValidade, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime dataExpiracao))
                {
                    var host = HttpContext.Request.Host.Value;
                    Ferramentas.LerArquivoCertificado(host + "=" + dataExpiracao.ToString("dd/MM/yyyy"));
                    return "Atualizado com Sucesso!";
                }
            }

            return "Erro.";
        }

        //[HttpPost]
        //public async Task<IActionResult> GerarCertificadoSite(string site, string email)
        //{
        //    string[] sites = { site };
        //    try
        //    {
        //        // Chama o método GenerateCertificate para gerar os certificados
        //        var (certPem, keyPem) = await Ferramentas.GenerateCertificate(domains: sites, email);

        //        // Cria um arquivo ZIP na memória para armazenar os certificados e chaves
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        //            {
        //                // Adiciona o certificado e a chave ao arquivo ZIP
        //                var certEntry = archive.CreateEntry("certificate.pem");
        //                using (var certStream = certEntry.Open())
        //                using (var writer = new StreamWriter(certStream))
        //                {
        //                    await writer.WriteAsync(certPem);
        //                }

        //                var keyEntry = archive.CreateEntry("private-key.pem");
        //                using (var keyStream = keyEntry.Open())
        //                using (var writer = new StreamWriter(keyStream))
        //                {
        //                    await writer.WriteAsync(keyPem);
        //                }
        //            }

        //            // Retorna o arquivo ZIP como um download para o cliente
        //            return File(memoryStream.ToArray(), "application/zip", "certificado.zip");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erro ao gerar o certificado: {ex.Message}");
        //    }
        //}
    }
}