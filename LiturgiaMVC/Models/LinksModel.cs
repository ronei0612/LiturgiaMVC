namespace LiturgiaMVC.Models
{
    public class LinksModel
    {
        public Dictionary<string, string>? LinksDict { get; set; }
        public string[]? Youtube { get; set; }
		public Dictionary<string, string>? YoutubeImageLinks { get; set; }
        public string[]? AcordesLinks { get; set; }

        public string[]? RelatorioLines { get; set; }
        public string? Tom { get; set; }
    }
}
