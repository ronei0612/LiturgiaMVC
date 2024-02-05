using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Net;
using Google.Apis.Auth.OAuth2.Responses;

namespace LiturgiaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Google Drive Upload";

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
            string accessToken = "ya29.a0AfB_byDZB7xre5vgmySDANXuaJ9i9dYeQwRo5EyICJat5yKDkfixvRgdfqEyYQfHssVOAovJsixP5IaeCZaGou7nQkw1ZlepvuHs0_dpDYRndUmC_Pyvkv0iBsaFaq548G-6nVW2BnAGA-3vOa8vidvajX4k22DsLQaCgYKAQ4SARASFQHGX2MiPAKzuCa9ViaJLpmt76VDwA0169";

            // Criar serviço do Google Drive
            //var service = new DriveService(new BaseClientService.Initializer()
            //{
            //    ApplicationName = "Google Drive Upload",
            //    HttpClientInitializer = new Google.Apis.Http.HttpClientInitializer
            //    {
            //        DefaultCredential = GoogleCredential.FromAccessToken(accessToken)
            //    }
            //});

            UserCredential credential = GoogleCredential.FromAccessToken(accessToken)
                .CreateScoped(DriveService.Scope.Drive)
                .UnderlyingCredential as UserCredential;

            var service = new DriveService(new BaseClientService.Initializer()
			{
                ApplicationName = "Google Drive Upload",
                HttpClientInitializer = credential,
            });

            // Fazer upload de um arquivo
            UploadFile(service, @"C:\Users\ronei\Documents\Debian\testeUpload.txt");
            //UploadFile(service, @"C:\Users\ronei\Documents\Debian\testeUpload.txt", "");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        static void UploadFile(DriveService service, string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath)
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "text/plain");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);
            Console.WriteLine("File Link: https://drive.google.com/file/d/" + file.Id + "/view");
        }
    }
}