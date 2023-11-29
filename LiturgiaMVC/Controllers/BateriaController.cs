using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
    public class BateriaController : Controller
    {
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}
