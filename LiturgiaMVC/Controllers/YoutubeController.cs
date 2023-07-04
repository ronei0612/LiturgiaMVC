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
        public IActionResult Index(string v = "", string t = "")
        {
			var linksModel = new LinksModel();
			var dictYoutube = new Dictionary<string, string>();
			var link = v;
			

			if (string.IsNullOrEmpty(v) == false)
			{
				if (v.Contains("live/")) {
					var canalid = Regex.Split(v, "live/")[1];
					if (canalid.Contains('?'))
						canalid = canalid.Split('?')[0];
					link += "live_stream?channel=" + canalid + "&rel=0&autoplay=1&loop=1";
				}
					
				else if (v.Contains('?'))
					link += "&rel=0&autoplay=1&loop=1";
				else
					link += "?rel=0&autoplay=1&loop=1";

				if (string.IsNullOrEmpty("t") == false)
					if (t == "0")
						link += "&start=1";
					else
						link += "&start=" + t;

				dictYoutube.Add("", "https://www.youtube.com/embed/" + link);
				linksModel.YoutubeImageLinks = dictYoutube;
			}

			return View(linksModel);
        }

		[HttpPost]
		public IActionResult GerarLink(string texto)
		{
			var linksModel = new LinksModel();
			var link = "";
			var dictYoutube = new Dictionary<string, string>();

			if (texto.Contains("list="))
			{ 
				link = Regex.Split(texto, "list=")[1];
				link = "videoseries?list=" + link;

				dictYoutube.Add("", "https://www.youtube.com/embed/" + link);
			}

			else if (texto.Contains("v="))
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

				dictYoutube.Add("", "https://www.youtube.com/embed/" + link);
			}

			else if (texto.Contains("youtu.be"))
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

				dictYoutube.Add("", "https://www.youtube.com/embed/" + link);
			}

			else
			{
				dictYoutube = BuscarVideos(texto);
			}


			linksModel.YoutubeImageLinks = dictYoutube;

			return View("Index", linksModel);
		}

		private Dictionary<string, string> BuscarVideos(string texto)
		{
			texto = WebUtility.UrlEncode(texto);
			var imagesLinksYoutube = new Dictionary<string, string>();

			var httpClient = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://www.youtube.com/results?search_query=" + texto);
			var response = httpClient.Send(request);
			var reader = new StreamReader(response.Content.ReadAsStream());
			var textoHtml = reader.ReadToEnd();

			var videos = textoHtml.Split("videoRenderer").Skip(1).ToArray();

			foreach (var video in videos) {
				var linkId = Regex.Split(video, "videoId\":\"")[1].Split('"')[0];
				var videoImage = Regex.Split(video, "\"url\":\"")[1].Split('\"')[0];

				if (imagesLinksYoutube.ContainsKey(videoImage) == false)
					imagesLinksYoutube.Add(videoImage, "https://www.youtube.com/embed/" + linkId);
			}

			var videosList = textoHtml.Split("playlistRenderer").Skip(1).ToArray();

			foreach (var videoList in videosList)
			{
				var linkId = Regex.Split(videoList, "playlistId\":\"")[1].Split('"')[0];
				var videoImage = Regex.Split(videoList, "\"url\":\"")[1].Split('\"')[0];

				if (imagesLinksYoutube.ContainsKey(videoImage) == false)
					imagesLinksYoutube.Add(videoImage, "https://www.youtube.com/embed/videoseries?list=" + linkId);
				//https://www.youtube.com/embed/videoseries?list=PLDD382858034CD2DF
			}

			return imagesLinksYoutube;
		}
	}
}
