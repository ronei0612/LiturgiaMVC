namespace LiturgiaMVC
{
    public class Variaveis
    {
        public static string pastaArquivos = "Arquivos";

        public static string links = "Links.txt";

        public static string IPs = "IPs.csv";

        public static string acordesLinksCsv = "AcordesLinks.txt";

        public static string acordesListaCsv = "AcordesLista.txt";

        public static string pastaSons = "./Sons/";

        public static string arquivoLinks = Path.Combine(pastaArquivos, links);

        public static string arquivoIPs = Path.Combine(pastaArquivos, IPs);

        public static string arquivoAcordesLinks = Path.Combine(pastaArquivos, acordesLinksCsv);

        public static string arquivoAcordesLista = Path.Combine(pastaArquivos, acordesListaCsv);

        public static string[] tonsMaiores, tonsMenores;

        public static readonly string[] acidentes = { "5+", "6", "7", "7+", "9", "11" };

        public static Dictionary<string, string> acordesLinks;

        public static Dictionary<string, string> acordesMenoresRelativos = new()
        {
            { "C", "Am" },
            { "D", "Bm" },
            { "E", "C#m" },
            { "F", "Dm" },
            { "G", "Em" },
            { "A", "F#m" },
            { "B", "G#m" },
        };

        public static readonly Dictionary<string, string[]> acordes = new()
        {
            { "C", new string[] { "C", "Dm", "Em", "F", "G", "Am", "Bb", "D", "Cm", "A", "E" } },
            { "D", new string[] { "D", "Em", "F#m", "G", "A", "Bm", "C", "E", "Dm", "B", "F#" } },
            { "E", new string[] { "E", "F#m", "G#m", "A", "B", "C#m", "D", "F#", "Em", "C#", "G#" } },
            { "F", new string[] { "F", "Gm", "Am", "Bb", "C", "Dm" , "Eb", "G", "Fm", "D", "A" } },
            { "G", new string[] { "G", "Am", "Bm", "C", "D", "Em" , "F", "A", "Gm", "E", "B" } },
            { "A", new string[] { "A", "Bm", "C#m", "D", "E", "F#m" , "G", "B", "Am", "F#", "C#" } },
            { "B", new string[] { "B", "C#m", "D#m", "E", "F#", "G#m" , "A", "C#", "Bm", "G#", "D#" } },
            { "Am", new string[] { "C", "Dm", "Em", "F", "G", "Am", "A", "E", "", "Am7", "Am9" } },
            { "Bm", new string[] { "D", "Em", "F#m", "G", "A", "Bm", "B", "F#", "", "Bm7", "Bm9" } },
            { "C#m", new string[] { "E", "F#m", "G#m", "A", "B", "Cm", "C#", "G#", "", "C#m7", "C#m9" } },
            { "Dm", new string[] { "F", "Gm", "Am", "Bb", "C", "Dm", "D", "A", "", "Dm7", "Dm9" } },
            { "Em", new string[] { "G", "Am", "Bm", "C", "D", "Em", "E", "B", "", "Em7", "Em9" } },
            { "F#m", new string[] { "A", "Bm", "C#m", "D", "E", "F#m", "F#", "C#", "", "F#m7", "F#m9" } },
            { "G#m", new string[] { "B", "C#m", "D#m", "E", "F#", "G#m", "G#", "D#", "", "G#m7", "G#m9" } }
        };
    }
}
