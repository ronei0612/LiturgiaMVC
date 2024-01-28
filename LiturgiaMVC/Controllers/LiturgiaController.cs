using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class LiturgiaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OracoesEucaristicas()
        {
            return View();
        }
    }
}
