namespace LiturgiaMVC
{
    public class Variaveis
    {
        public static string pastaArquivos = "Arquivos";
        public static string pastaSons = "./Sons/";

        static string links = "Links.txt";
        static string IPs = "IPs.csv";
        static string acordesLinksCsv = "AcordesLinks.txt";
        static string acordesListaCsv = "AcordesLista.txt";
        static string ritmosBateriaJson = "RitmosLista.txt";
        static string notasAcordesJson = "NotasAcordes.txt";

        public static string arquivoLinks = Path.Combine(pastaArquivos, links);
        public static string arquivoIPs = Path.Combine(pastaArquivos, IPs);
        public static string arquivoAcordesLinks = Path.Combine(pastaArquivos, acordesLinksCsv);
        public static string arquivoAcordesLista = Path.Combine(pastaArquivos, acordesListaCsv);
        public static string arquivoRitmosBateria = Path.Combine(pastaArquivos, ritmosBateriaJson);
        public static string arquivonotasAcordes = Path.Combine(pastaArquivos, notasAcordesJson);

        public static string[] tonsMaiores, tonsMenores;

        public static readonly string[] acidentes = { "5+", "6", "7", "7+", "9", "11" };

        public static string textoRitmos = "";
        public static string textoNotasAcordes = "";

        public static Dictionary<string, string> acordesLinks;

        public static Dictionary<string, string> acordesMenoresRelativos = new()
        {
            { "C", "Am" },
            { "C#", "A#m" },
            { "D", "Bm" },
            { "D#", "Cm" },
            { "E", "C#m" },
            { "F", "Dm" },
            { "F#", "D#m" },
            { "G", "Em" },
            { "G#", "Fm" },
            { "A", "F#m" },
            { "A#", "Gm" },
            { "B", "G#m" }
        };

        public static Dictionary<string, string> acidentesCorrespondentes = new()
        {
            { "Db", "C#" },
            { "Eb", "D#" },
            { "Ab", "G#" },
            { "Bb", "A#" },
            { "E#", "F" },
            { "B#", "C" },
            { "Fb", "E" },
            { "Cb", "B" }
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

        public static readonly Dictionary<string, string[]> notasAcordes = new()
        {
            { "C", new string[] { "do", "mi", "sol" } },
            { "Cm", new string[] { "do", "re#", "sol" } },
            { "C#", new string[] { "do#", "fa", "sol#" } },
            { "C#m", new string[] { "do#", "mi", "sol#" } },
            { "D", new string[] { "re", "fa#", "la", } },
            { "Dm", new string[] { "re", "fa", "la", } },
            { "D#", new string[] { "re#", "sol", "la#", } },
            { "D#m", new string[] { "re#", "fa#", "la#", } },
            { "E", new string[] { "mi", "sol#", "si" } },
            { "Em", new string[] { "mi", "sol", "si" } },
            { "F", new string[] { "fa", "la", "do" } },
            { "Fm", new string[] { "fa", "sol#", "do" } },
            { "F#", new string[] { "fa#", "la#", "do#" } },
            { "F#m", new string[] { "fa#", "la", "do#" } },
            { "G", new string[] { "sol", "si", "re" } },
            { "Gm", new string[] { "sol", "la#", "re" } },
            { "G#", new string[] { "sol#", "do", "re#" } },
            { "G#m", new string[] { "sol#", "si", "re#" } },
            { "A", new string[] { "la", "do#", "mi" } },
            { "Am", new string[] { "la", "do", "mi" } },
            { "A#", new string[] { "la#", "re", "fa" } },
            { "A#m", new string[] { "la#", "do#", "fa" } },
            { "B", new string[] { "si", "re#", "fa#" } },
            { "Bm", new string[] { "si", "re", "fa#" } }
        };
    }
}
