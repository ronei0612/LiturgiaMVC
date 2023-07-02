using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System;
using LiturgiaMVC.Models;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace LiturgiaMVC.Controllers
{
    public class YoutubeController : Controller
    {
		[HttpGet]
        public IActionResult Index(string v = "")
        {
			var linksModel = new LinksModel();
			var link = v;

			if (string.IsNullOrEmpty(v) == false)
			{
				if (v.Contains('?'))
					link += "&rel=0&autoplay=1&loop=1";
				else
					link += "?rel=0&autoplay=1&loop=1";

				linksModel.Youtube = "https://www.youtube.com/embed/" + link;
			}

			return View(linksModel);
        }

		[HttpPost]
		public IActionResult GerarLink(string texto)
		{
			var linksModel = new LinksModel();
			var link = "";

			if (texto.Contains("list=")) //youtube.com/watch?v=0xNzlVHaEXY&list=PL_1F_fsE9bd6VY9DeflOcdqGld62uyt90
			{ 
				link = Regex.Split(texto, "list=")[1];
				link = "videoseries?list=" + link; //link = videoseries?list=PLc2L-_BdS7A5SHoqM4ZEMD95Prw1-LmRg&index=7
			}

			else if (texto.Contains("v=")) //youtube.com/watch?v=sTTnkDKQIjM
			{ 
				link = Regex.Split(texto, "v=")[1];

				if (link.Contains('&'))
				{
					try
					{
						link = link.Split('&')[0];
					}
					catch { }
				}
			}

			else if (texto.Contains("youtu.be")) //youtu.be/sTTnkDKQIjM
			{ 
				link = Regex.Split(texto, ".be/")[1];

				if (link.Contains('&'))
				{
					try
					{
						link = link.Split('&')[0];
					}
					catch { }
				}
			}

			linksModel.Youtube = "https://www.youtube.com/embed/" + link;

			return View(linksModel);
		}

		
	}
}
