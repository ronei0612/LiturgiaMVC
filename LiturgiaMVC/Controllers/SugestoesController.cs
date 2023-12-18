using Microsoft.AspNetCore.Mvc;

namespace LiturgiaMVC.Controllers
{
	public class SugestoesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
