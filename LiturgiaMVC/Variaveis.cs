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

        public static readonly string[] acidentes = { "5+", "6", "7", "7+", "9", "11" };
        public static string[] tonsMaiores, tonsMenores;

        public static string textoRitmos = "";
        public static string textoNotasAcordes = "";

        public static Dictionary<string, string> acordesLinks;
        public static Dictionary<string, string> notasLinks;

        public static string tonsMaioresString = "C, C#, D, D#, E, F, F#, G, G#, A, A#, B";
        public static string tonsMenoresString = "Am, A#m, Bm, Cm, C#m, Dm, D#m, Em, Fm, F#m, Gm, G#m";

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
            { "B", "G#m" }
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
            { "D#", new string[] { "D#", "Fm", "Gm", "G#", "A#", "Cm", "C#", "F", "D#m", "C", "G" } },
            { "E", new string[] { "E", "F#m", "G#m", "A", "B", "C#m", "D", "F#", "Em", "C#", "G#" } },
            { "F", new string[] { "F", "Gm", "Am", "Bb", "C", "Dm" , "Eb", "G", "Fm", "D", "A" } },
            { "F#", new string[] { "F#", "G#m", "A#m", "B", "C#", "D#m" , "E", "G#", "F#m", "D#", "A#" } },
            { "G", new string[] { "G", "Am", "Bm", "C", "D", "Em" , "F", "A", "Gm", "E", "B" } },
            { "G#", new string[] { "G#", "Bbm", "Cm", "C#", "D#", "Fm" , "F#", "A#", "G#m", "F", "C" } },
            { "A", new string[] { "A", "Bm", "C#m", "D", "E", "F#m" , "G", "B", "Am", "F#", "C#" } },            
            { "A#", new string[] { "A#", "Cm", "Dm", "D#", "F", "Gm" , "G#", "C", "A#m", "G", "D" } },
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
            { "C4", new string[] { "do", "fa", "sol" } },
            { "C5+", new string[] { "do", "mi", "sol#" } },
            { "C6", new string[] { "do", "mi", "sol", "la" } },
            { "C7", new string[] { "do", "mi", "sol", "la#" } },
            { "C7M", new string[] { "do", "mi", "sol", "si" } },
            { "C9", new string[] { "do", "mi", "sol", "re" } },
            { "C°", new string[] { "do", "re#", "fa#" } },
            { "C°7", new string[] { "do", "re#", "fa#", "la#" } },
            { "Cm", new string[] { "do", "re#", "sol" } },
            { "Cm5+", new string[] { "do", "re#", "sol#" } },
            { "Cm6", new string[] { "do", "re#", "sol", "la" } },
            { "Cm7", new string[] { "do", "re#", "sol", "la#" } },
            { "Cm7M", new string[] { "do", "re#", "sol", "si" } },

            { "C#", new string[] { "do#", "fa", "sol#" } },
            { "C#4", new string[] { "do#", "fa#", "sol#" } },
            { "C#5+", new string[] { "do#", "fa", "la" } },
            { "C#6", new string[] { "do#", "fa", "sol#", "la#" } },
            { "C#7", new string[] { "do#", "fa", "sol#", "si" } },
            { "C#7M", new string[] { "do#", "fa", "sol#", "do" } },
            { "C#9", new string[] { "do#", "fa", "sol#", "re#" } },
            { "C#°", new string[] { "do#", "mi", "sol" } },
            { "C#°7", new string[] { "do#", "mi", "sol", "si" } },
            { "C#m", new string[] { "do#", "mi", "sol#" } },
            { "C#m5+", new string[] { "do#", "mi", "la" } },
            { "C#m6", new string[] { "do#", "mi", "sol#", "la#" } },
            { "C#m7", new string[] { "do#", "mi", "sol#", "si" } },
            { "C#m7M", new string[] { "do#", "mi", "sol#", "do" } },

            { "D", new string[] { "re", "fa#", "la" } },
            { "D4", new string[] { "re", "sol", "la" } },
            { "D5+", new string[] { "re", "fa#", "la#" } },
            { "D6", new string[] { "re", "fa#", "la", "si" } },
            { "D7", new string[] { "re", "fa#", "la", "do" } },
            { "D7M", new string[] { "re", "fa#", "la", "do#" } },
            { "D9", new string[] { "re", "fa#", "la", "mi" } },
            { "D°", new string[] { "re", "fa", "sol#" } },
            { "D°7", new string[] { "re", "fa", "sol#", "do" } },
            { "Dm", new string[] { "re", "fa", "la" } },
            { "Dm5+", new string[] { "re", "fa", "la#" } },
            { "Dm6", new string[] { "re", "fa", "la", "si" } },
            { "Dm7", new string[] { "re", "fa", "la", "do" } },
            { "Dm7M", new string[] { "re", "fa", "la", "do#" } },

            { "D#", new string[] { "re#", "sol", "la#" } },
            { "D#4", new string[] { "re#", "sol#", "la#" } },
            { "D#5+", new string[] { "re#", "sol", "si" } },
            { "D#6", new string[] { "re#", "sol", "la#", "do" } },
            { "D#7", new string[] { "re#", "sol", "la#", "do#" } },
            { "D#7M", new string[] { "re#", "sol", "la#", "re" } },
            { "D#9", new string[] { "re#", "sol", "la#", "fa" } },
            { "D#°", new string[] { "re#", "fa#", "la" } },
            { "D#°7", new string[] { "re#", "fa#", "la", "do#" } },
            { "D#m", new string[] { "re#", "fa#", "la#" } },
            { "D#m5+", new string[] { "re#", "fa#", "si" } },
            { "D#m6", new string[] { "re#", "fa#", "la#", "do" } },
            { "D#m7", new string[] { "re#", "fa#", "la#", "do#" } },
            { "D#m7M", new string[] { "re#", "fa#", "la#", "re" } },

            { "E", new string[] { "mi", "sol#", "si" } },
            { "E4", new string[] { "mi", "la", "si" } },
            { "E5+", new string[] { "mi", "sol#", "do" } },
            { "E6", new string[] { "mi", "sol#", "si", "do#" } },
            { "E7", new string[] { "mi", "sol#", "si", "re" } },
            { "E7M", new string[] { "mi", "sol#", "si", "re#" } },
            { "E9", new string[] { "mi", "sol#", "si", "fa#" } },
            { "E°", new string[] { "mi", "sol", "la#" } },
            { "E°7", new string[] { "mi", "sol", "la#", "re" } },
            { "Em", new string[] { "mi", "sol", "si" } },
            { "Em5+", new string[] { "mi", "sol", "do" } },
            { "Em6", new string[] { "mi", "sol", "si", "do#" } },
            { "Em7", new string[] { "mi", "sol", "si", "re" } },
            { "Em7M", new string[] { "mi", "sol", "si", "re#" } },

            { "F", new string[] { "fa", "la", "do" } },
            { "F4", new string[] { "fa", "la#", "do" } },
            { "F5+", new string[] { "fa", "la", "do#" } },
            { "F6", new string[] { "fa", "la", "do", "re" } },
            { "F7", new string[] { "fa", "la", "do", "re#" } },
            { "F7M", new string[] { "fa", "la", "do", "mi" } },
            { "F9", new string[] { "fa", "la", "do", "sol" } },
            { "F°", new string[] { "fa", "sol#", "si" } },
            { "F°7", new string[] { "fa", "sol#", "si", "re#" } },
            { "Fm", new string[] { "fa", "sol#", "do" } },
            { "Fm5+", new string[] { "fa", "sol#", "do#" } },
            { "Fm6", new string[] { "fa", "sol#", "do", "re" } },
            { "Fm7", new string[] { "fa", "sol#", "do", "re#" } },
            { "Fm7M", new string[] { "fa", "sol#", "do", "mi" } },

            { "F#", new string[] { "fa#", "la#", "do#" } },
            { "F#4", new string[] { "fa#", "si", "do#" } },
            { "F#5+", new string[] { "fa#", "la#", "re" } },
            { "F#6", new string[] { "fa#", "la#", "do#", "re#" } },
            { "F#7", new string[] { "fa#", "la#", "do#", "mi" } },
            { "F#7M", new string[] { "fa#", "la#", "do#", "fa" } },
            { "F#9", new string[] { "fa#", "la#", "do#", "sol#" } },
            { "F#°", new string[] { "fa#", "la", "do" } },
            { "F#°7", new string[] { "fa#", "la", "do", "mi" } },
            { "F#m", new string[] { "fa#", "la", "do#" } },
            { "F#m5+", new string[] { "fa#", "la", "re" } },
            { "F#m6", new string[] { "fa#", "la", "do#", "re#" } },
            { "F#m7", new string[] { "fa#", "la", "do#", "mi" } },
            { "F#m7M", new string[] { "fa#", "la", "do#", "fa" } },

            { "G", new string[] { "sol", "si", "re" } },
            { "G4", new string[] { "sol", "do", "re" } },
            { "G5+", new string[] { "sol", "si", "re#" } },
            { "G6", new string[] { "sol", "si", "re", "mi" } },
            { "G7", new string[] { "sol", "si", "re", "fa" } },
            { "G7M", new string[] { "sol", "si", "re", "fa#" } },
            { "G9", new string[] { "sol", "si", "re", "la" } },
            { "G°", new string[] { "sol", "la#", "do#" } },
            { "G°7", new string[] { "sol", "la#", "do#", "fa" } },
            { "Gm", new string[] { "sol", "la#", "re" } },
            { "Gm5+", new string[] { "sol", "la#", "re#" } },
            { "Gm6", new string[] { "sol", "la#", "re", "mi" } },
            { "Gm7", new string[] { "sol", "la#", "re", "fa" } },
            { "Gm7M", new string[] { "sol", "la#", "re", "fa#" } },

            { "G#", new string[] { "sol#", "do", "re#" } },
            { "G#4", new string[] { "sol#", "do#", "re#" } },
            { "G#5+", new string[] { "sol#", "do", "mi" } },
            { "G#6", new string[] { "sol#", "do", "re#", "fa" } },
            { "G#7", new string[] { "sol#", "do", "re#", "fa#" } },
            { "G#7M", new string[] { "sol#", "do", "re#", "sol" } },
            { "G#9", new string[] { "sol#", "do", "re#", "la#" } },
            { "G#°", new string[] { "sol#", "si", "re" } },
            { "G#°7", new string[] { "sol#", "si", "re", "fa#" } },
            { "G#m", new string[] { "sol#", "si", "re#" } },
            { "G#m5+", new string[] { "sol#", "si", "mi" } },
            { "G#m6", new string[] { "sol#", "si", "re#", "fa" } },
            { "G#m7", new string[] { "sol#", "si", "re#", "fa#" } },
            { "G#m7M", new string[] { "sol#", "si", "re#", "sol" } },

            { "A", new string[] { "la", "do#", "mi" } },
            { "A4", new string[] { "la", "re", "mi" } },
            { "A5+", new string[] { "la", "do#", "fa" } },
            { "A6", new string[] { "la", "do#", "mi", "fa#" } },
            { "A7", new string[] { "la", "do#", "mi", "sol" } },
            { "A7M", new string[] { "la", "do#", "mi", "sol#" } },
            { "A9", new string[] { "la", "do#", "mi", "si" } },
            { "A°", new string[] { "la", "do", "re#" } },
            { "A°7", new string[] { "la", "do", "re#", "sol" } },
            { "Am", new string[] { "la", "do", "mi" } },
            { "Am5+", new string[] { "la", "do", "fa" } },
            { "Am6", new string[] { "la", "do", "mi", "fa#" } },
            { "Am7", new string[] { "la", "do", "mi", "sol" } },
            { "Am7M", new string[] { "la", "do", "mi", "sol#" } },

            { "A#", new string[] { "la#", "re", "fa" } },
            { "A#4", new string[] { "la#", "re#", "fa" } },
            { "A#5+", new string[] { "la#", "re", "fa#" } },
            { "A#6", new string[] { "la#", "re", "fa", "sol" } },
            { "A#7", new string[] { "la#", "re", "fa", "sol#" } },
            { "A#7M", new string[] { "la#", "re", "fa", "la" } },
            { "A#9", new string[] { "la#", "re", "fa", "do" } },
            { "A#°", new string[] { "la#", "do#", "mi" } },
            { "A#°7", new string[] { "la#", "do#", "mi", "sol#" } },
            { "A#m", new string[] { "la#", "do#", "fa" } },
            { "A#m5+", new string[] { "la#", "do#", "fa#" } },
            { "A#m6", new string[] { "la#", "do#", "fa", "sol" } },
            { "A#m7", new string[] { "la#", "do#", "fa", "sol#" } },
            { "A#m7M", new string[] { "la#", "do#", "fa", "la" } },

            { "B", new string[] { "si", "re#", "fa#" } },
            { "B4", new string[] { "si", "mi", "fa#" } },
            { "B5+", new string[] { "si", "re#", "sol" } },
            { "B6", new string[] { "si", "re#", "fa#", "sol#" } },
            { "B7", new string[] { "si", "re#", "fa#", "la" } },
            { "B7M", new string[] { "si", "re#", "fa#", "la#" } },
            { "B9", new string[] { "si", "re#", "fa#", "do#" } },
            { "B°", new string[] { "si", "re", "fa" } },
            { "B°7", new string[] { "si", "re", "fa", "la" } },
            { "Bm", new string[] { "si", "re", "fa#" } },
            { "Bm5+", new string[] { "si", "re", "sol" } },
            { "Bm6", new string[] { "si", "re", "fa#", "sol#" } },
            { "Bm7", new string[] { "si", "re", "fa#", "la" } },
            { "Bm7M", new string[] { "si", "re", "fa#", "la#" } },
        };
    }
}
