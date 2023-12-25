namespace LiturgiaMVC
{
    public class Variaveis
    {
        public static string pastaArquivos = "Arquivos";
        public static string pastaSons = "./Sons/";
        public static string pastaSonsOrgao = "./Sons/Orgao/";
        public static string pastaSonsStrings = "./Sons/Strings/";

        static string links = "Links.txt";
        static string IPs = "IPs.csv";
        static string acordesLinksCsv = "AcordesLinks.txt";
        static string notasLinksCsv = "NotasLinks.txt";
        static string acordesListaCsv = "AcordesLista.txt";
        static string ritmosBateriaJson = "RitmosLista.txt";
        static string notasAcordesJson = "NotasAcordes.txt";

        public static string arquivoLinks = Path.Combine(pastaArquivos, links);
        public static string arquivoIPs = Path.Combine(pastaArquivos, IPs);
        public static string arquivoAcordesLinks = Path.Combine(pastaArquivos, acordesLinksCsv);
        public static string arquivoNotasLinks = Path.Combine(pastaArquivos, notasLinksCsv);
        public static string arquivoAcordesLista = Path.Combine(pastaArquivos, acordesListaCsv);
        public static string arquivoRitmosBateria = Path.Combine(pastaArquivos, ritmosBateriaJson);
        public static string arquivonotasAcordes = Path.Combine(pastaArquivos, notasAcordesJson);

        public static string[] tonsMaiores, tonsMenores;

        public static readonly string[] acidentes = { "5+", "6", "7", "7+", "9", "11" };

        public static string textoRitmos = "";
        public static string textoNotasAcordes = "";

        public static Dictionary<string, string> acordesLinks;
        public static Dictionary<string, string> notasLinks;

        public static Dictionary<string, string> acordesMenoresRelativos = new()
        {
            { "C", "Am" },
            { "C#", "A#m" },
            { "D", "Bm" },
            { "Eb", "Cm" },
            { "D#", "Cm" },
            { "E", "C#m" },
            { "F", "Dm" },
            { "F#", "D#m" },
            { "G", "Em" },
            { "Ab", "Fm" },
            { "G#", "Fm" },
            { "A", "F#m" },
            { "Bb", "Gm" },
            { "A#", "Gm" },
            { "B", "G#m" },
        };

        public static Dictionary<string, string> acidentesCorrespondentes = new()
        {
            { "C", "C" },
            { "C#", "C#" },
            { "Db", "C#" },
            { "D", "D" },
            { "D#", "D#" },
            { "Eb", "D#" },
            { "E", "E" },
            { "Fb", "E" },
            { "F", "F" },
            { "E#", "F" },
            { "F#", "F#" },
            { "G", "G" },
            { "G#", "G#" },
            { "Ab", "G#" },            
            { "A", "A" },
            { "A#", "A#" },
            { "Bb", "A#" },
            { "B", "B" },
            { "B#", "C" },
            { "Cb", "B" }
        };

        public static readonly Dictionary<string, string[]> acordes = new()
        {
            { "C", new string[] { "C", "Dm", "Em", "F", "G", "Am", "Bb", "D", "Cm", "A", "E" } },
            { "C#", new string[] { "C#", "D#m", "E#m", "F#", "G#", "A#m", "B", "D#", "C#m", "A#", "E#" } },
            { "D", new string[] { "D", "Em", "F#m", "G", "A", "Bm", "C", "E", "Dm", "B", "F#" } },
            { "Eb", new string[] { "Eb", "Fm", "Gm", "Ab", "Bb", "Cm", "Db", "F", "Ebm", "C", "G" } },
            { "E", new string[] { "E", "F#m", "G#m", "A", "B", "C#m", "D", "F#", "Em", "C#", "G#" } },
            { "F", new string[] { "F", "Gm", "Am", "Bb", "C", "Dm" , "Eb", "G", "Fm", "D", "A" } },
            { "F#", new string[] { "F#", "G#m", "A#m", "B", "C#", "D#m" , "E", "G#", "F#m", "D#", "A#" } },
            { "G", new string[] { "G", "Am", "Bm", "C", "D", "Em" , "F", "A", "Gm", "E", "B" } },
            { "Ab", new string[] { "Ab", "Bbm", "Cm", "Db", "Eb", "Fm" , "Gb", "Bb", "Abm", "F", "C" } },
            { "A", new string[] { "A", "Bm", "C#m", "D", "E", "F#m" , "G", "B", "Am", "F#", "C#" } },            
            { "Bb", new string[] { "Bb", "Cm", "Dm", "Eb", "F", "Gm" , "Ab", "C", "Bbm", "G", "D" } },
            { "B", new string[] { "B", "C#m", "D#m", "E", "F#", "G#m" , "A", "C#", "Bm", "G#", "D#" } },

            { "Am", new string[] { "C", "Dm", "Em", "F", "G", "Am", "A", "E", "", "", "" } },
            { "A#m", new string[] { "C#", "D#m", "E#m", "F#", "G#", "A#m", "A#", "E#", "", "", "" } },
            { "Bm", new string[] { "D", "Em", "F#m", "G", "A", "Bm", "B", "F#", "", "", "" } },
            { "Cm", new string[] { "Eb", "Fm", "Gm", "Ab", "Bb", "Cm", "C", "G", "", "", "" } },
            { "C#m", new string[] { "E", "F#m", "G#m", "A", "B", "Cm", "C#", "G#", "", "", "" } },
            { "Dm", new string[] { "F", "Gm", "Am", "Bb", "C", "Dm", "D", "A", "", "", "" } },
            { "D#m", new string[] { "F#", "G#m", "A#m", "C", "C#", "D#m", "D#", "A#", "", "", "" } },
            { "Em", new string[] { "G", "Am", "Bm", "C", "D", "Em", "E", "B", "", "", "" } },
            { "Fm", new string[] { "Ab", "Bbm", "Cm", "Db", "Eb", "Fm", "F", "C", "", "", "" } },
            { "F#m", new string[] { "A", "Bm", "C#m", "D", "E", "F#m", "F#", "C#", "", "", "" } },
            { "Gm", new string[] { "Bb", "Cm", "Dm", "Eb", "F", "Gm", "G", "D", "", "", "" } },
            { "G#m", new string[] { "B", "C#m", "D#m", "E", "F#", "G#m", "G#", "D#", "", "", "" } }
        };

        public static readonly Dictionary<string, string[]> notasAcordes = new()
        {
            { "C", new string[] { "do", "mi", "sol" } },
            { "Cm", new string[] { "do", "re#", "sol" } },
            { "C#", new string[] { "do#", "fa", "sol#" } },
            { "C#m", new string[] { "do#", "mi", "sol#" } },
            { "Db", new string[] { "do#", "fa", "sol#" } },
            { "Dbm", new string[] { "do#", "mi", "sol#" } },
            { "D", new string[] { "re", "fa#", "la", } },
            { "Dm", new string[] { "re", "fa", "la", } },
            { "D#", new string[] { "re#", "sol", "la#", } },
            { "D#m", new string[] { "re#", "fa#", "la#", } },
            { "Eb", new string[] { "re#", "sol", "la#", } },
            { "Ebm", new string[] { "re#", "fa#", "la#", } },
            { "E", new string[] { "mi", "sol#", "si" } },
            { "Em", new string[] { "mi", "sol", "si" } },
            { "E#", new string[] { "fa", "la", "do" } },
            { "E#m", new string[] { "fa", "sol#", "do" } },
            { "F", new string[] { "fa", "la", "do" } },
            { "Fm", new string[] { "fa", "sol#", "do" } },
            { "F#", new string[] { "fa#", "la#", "do#" } },
            { "F#m", new string[] { "fa#", "la", "do#" } },
            { "Gb", new string[] { "fa#", "la#", "do#" } },
            { "Gbm", new string[] { "fa#", "la", "do#" } },
            { "G", new string[] { "sol", "si", "re" } },
            { "Gm", new string[] { "sol", "la#", "re" } },
            { "G#", new string[] { "sol#", "do", "re#" } },
            { "G#m", new string[] { "sol#", "si", "re#" } },
            { "Ab", new string[] { "sol#", "do", "re#" } },
            { "Abm", new string[] { "sol#", "si", "re#" } },
            { "A", new string[] { "la", "do#", "mi" } },
            { "Am", new string[] { "la", "do", "mi" } },
            { "A#", new string[] { "la#", "re", "fa" } },
            { "A#m", new string[] { "la#", "do#", "fa" } },
            { "B", new string[] { "si", "re#", "fa#" } },
            { "Bm", new string[] { "si", "re", "fa#" } },
            { "Bb", new string[] { "la#", "re", "fa" } },
            { "Bbm", new string[] { "la#", "do#", "fa" } },
        };
    }
}
