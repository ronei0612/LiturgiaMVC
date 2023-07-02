using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System;
using LiturgiaMVC.Models;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Net;

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

				string[] arr = { "https://www.youtube.com/embed/" + link };
				linksModel.Youtube = arr;
			}

			return View(linksModel);
        }

		[HttpPost]
		public IActionResult GerarLink(string texto)
		{
			var linksModel = new LinksModel();
			var link = "";
			List<string> listYoutube = new List<string>();

			if (texto.Contains("list=")) //youtube.com/watch?v=0xNzlVHaEXY&list=PL_1F_fsE9bd6VY9DeflOcdqGld62uyt90
			{ 
				link = Regex.Split(texto, "list=")[1];
				link = "videoseries?list=" + link; //link = videoseries?list=PLc2L-_BdS7A5SHoqM4ZEMD95Prw1-LmRg&index=7

				listYoutube.Add("https://www.youtube.com/embed/" + link);
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

				listYoutube.Add("https://www.youtube.com/embed/" + link);
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

				listYoutube.Add("https://www.youtube.com/embed/" + link);
			}

			else
			{
				listYoutube = BuscarVideos(texto);
			}

			
			linksModel.Youtube = listYoutube.ToArray();

			return View("Index", linksModel);
		}

		private List<string> BuscarVideos(string texto)
		{
			texto = WebUtility.UrlEncode(texto);
			var imagesLinksYoutube = new List<string>();

			var httpClient = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://www.youtube.com/results?search_query=" + texto);
			var response = httpClient.Send(request);
			var reader = new StreamReader(response.Content.ReadAsStream());
			var textoHtml = reader.ReadToEnd();

			var videos = textoHtml.Split("videoRenderer").Skip(1).ToArray();

			foreach (var video in videos) {
				var linkId = Regex.Split(video, "videoId\":\"")[1].Split('"')[0];
				//var videoImage = Regex.Split(video, "\"url\":\"")[1].Split('\"')[0];

				imagesLinksYoutube.Add("https://www.youtube.com/embed/" + linkId);
			}

			return imagesLinksYoutube;

			//YoutubeDict

			//var client = new WebClient();
			//string reply = client.DownloadString(address);

			//Console.WriteLine(reply);
		}
	}
}
