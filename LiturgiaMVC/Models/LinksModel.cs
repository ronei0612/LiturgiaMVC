﻿namespace LiturgiaMVC.Models
{
    public class LinksModel
    {
        public Dictionary<string, string>? LinksDict { get; set; }
        public string[]? Youtube { get; set; }
		public Dictionary<string, string>? YoutubeImageLinks { get; set; }
        public string[]? Acordes { get; set; }

        public string[]? RelatorioLines { get; set; }
        public string? Tom { get; set; }
        public int? TomIndex { get; set; }
        public string[]? TonsMaiores { get; set; }
        public string[]? TonsMenores { get; set; }
        public string? RitmosBateria { get; set; }
    }
}
