using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class YoutubeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
