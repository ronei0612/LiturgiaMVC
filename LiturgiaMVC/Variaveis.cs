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
            { "Cm", new string[] { "do", "re#", "sol" } },
            { "Cm5+", new string[] { "do", "re#", "sol#" } },
            { "Cm6", new string[] { "do", "re#", "sol", "la" } },
            { "Cm7", new string[] { "do", "re#", "sol", "la#" } },
            { "Cm7M", new string[] { "do", "re#", "sol", "si" } },
            { "C°", new string[] { "do", "re#", "fa#" } },
            { "C°7", new string[] { "do", "re#", "fa#", "la#" } },

            { "C#", new string[] { "C#do", "mi", "sol" } },
            { "C#4", new string[] { "C#do", "mi", "sol" } },
            { "C#5+", new string[] { "C#do", "mi", "sol" } },
            { "C#6", new string[] { "C#do", "mi", "sol" } },
            { "C#7", new string[] { "C#do", "mi", "sol" } },
            { "C#7M", new string[] { "C#do", "mi", "sol" } },
            { "C#9", new string[] { "C#do", "mi", "sol" } },
            { "C#m", new string[] { "C#do", "mi", "sol" } },
            { "C#m5+", new string[] { "C#do", "mi", "sol" } },
            { "C#m6", new string[] { "C#do", "mi", "sol" } },
            { "C#m7", new string[] { "C#do", "mi", "sol" } },
            { "C#m7M", new string[] { "C#do", "mi", "sol" } },
            { "C#°", new string[] { "C#do", "mi", "sol" } },

            { "D", new string[] { "do", "mi", "sol" } },
            { "D4", new string[] { "do", "mi", "sol" } },
            { "D5+", new string[] { "do", "mi", "sol" } },
            { "D6", new string[] { "do", "mi", "sol" } },
            { "D7", new string[] { "do", "mi", "sol" } },
            { "D7M", new string[] { "do", "mi", "sol" } },
            { "D9", new string[] { "do", "mi", "sol" } },
            { "Dm", new string[] { "do", "mi", "sol" } },
            { "Dm5+", new string[] { "do", "mi", "sol" } },
            { "Dm6", new string[] { "do", "mi", "sol" } },
            { "Dm7", new string[] { "do", "mi", "sol" } },
            { "Dm7M", new string[] { "do", "mi", "sol" } },
            { "D°", new string[] { "do", "mi", "sol" } },

            { "D#", new string[] { "D#o", "mi", "sol" } },
            { "D#4", new string[] { "D#o", "mi", "sol" } },
            { "D#5+", new string[] { "D#o", "mi", "sol" } },
            { "D#6", new string[] { "D#o", "mi", "sol" } },
            { "D#7", new string[] { "D#o", "mi", "sol" } },
            { "D#7M", new string[] { "D#o", "mi", "sol" } },
            { "D#9", new string[] { "D#o", "mi", "sol" } },
            { "D#m", new string[] { "D#o", "mi", "sol" } },
            { "D#m5+", new string[] { "D#o", "mi", "sol" } },
            { "D#m6", new string[] { "D#o", "mi", "sol" } },
            { "D#m7", new string[] { "D#o", "mi", "sol" } },
            { "D#m7M", new string[] { "D#o", "mi", "sol" } },
            { "D#°", new string[] { "D#o", "mi", "sol" } },

            { "E", new string[] { "Eo", "mi", "sol" } },
            { "E4", new string[] { "Eo", "mi", "sol" } },
            { "E5+", new string[] { "Eo", "mi", "sol" } },
            { "E6", new string[] { "Eo", "mi", "sol" } },
            { "E7", new string[] { "Eo", "mi", "sol" } },
            { "E7M", new string[] { "Eo", "mi", "sol" } },
            { "E9", new string[] { "Eo", "mi", "sol" } },
            { "Em", new string[] { "Eo", "mi", "sol" } },
            { "Em5+", new string[] { "Eo", "mi", "sol" } },
            { "Em6", new string[] { "Eo", "mi", "sol" } },
            { "Em7", new string[] { "Eo", "mi", "sol" } },
            { "Em7M", new string[] { "Eo", "mi", "sol" } },
            { "E°", new string[] { "Eo", "mi", "sol" } },

            { "F", new string[] { "Fo", "mi", "sol" } },
            { "F4", new string[] { "Fo", "mi", "sol" } },
            { "F5+", new string[] { "Fo", "mi", "sol" } },
            { "F6", new string[] { "Fo", "mi", "sol" } },
            { "F7", new string[] { "Fo", "mi", "sol" } },
            { "F7M", new string[] { "Fo", "mi", "sol" } },
            { "F9", new string[] { "Fo", "mi", "sol" } },
            { "Fm", new string[] { "Fo", "mi", "sol" } },
            { "Fm5+", new string[] { "Fo", "mi", "sol" } },
            { "Fm6", new string[] { "Fo", "mi", "sol" } },
            { "Fm7", new string[] { "Fo", "mi", "sol" } },
            { "Fm7M", new string[] { "Fo", "mi", "sol" } },
            { "F°", new string[] { "Fo", "mi", "sol" } },

            { "F#", new string[] { "F#o", "mi", "sol" } },
            { "F#4", new string[] { "F#o", "mi", "sol" } },
            { "F#5+", new string[] { "F#o", "mi", "sol" } },
            { "F#6", new string[] { "F#o", "mi", "sol" } },
            { "F#7", new string[] { "F#o", "mi", "sol" } },
            { "F#7M", new string[] { "F#o", "mi", "sol" } },
            { "F#9", new string[] { "F#o", "mi", "sol" } },
            { "F#m", new string[] { "F#o", "mi", "sol" } },
            { "F#m5+", new string[] { "F#o", "mi", "sol" } },
            { "F#m6", new string[] { "F#o", "mi", "sol" } },
            { "F#m7", new string[] { "F#o", "mi", "sol" } },
            { "F#m7M", new string[] { "F#o", "mi", "sol" } },
            { "F#°", new string[] { "F#o", "mi", "sol" } },

            { "G", new string[] { "Go", "mi", "sol" } },
            { "G4", new string[] { "Go", "mi", "sol" } },
            { "G5+", new string[] { "Go", "mi", "sol" } },
            { "G6", new string[] { "Go", "mi", "sol" } },
            { "G7", new string[] { "Go", "mi", "sol" } },
            { "G7M", new string[] { "Go", "mi", "sol" } },
            { "G9", new string[] { "Go", "mi", "sol" } },
            { "Gm", new string[] { "Go", "mi", "sol" } },
            { "Gm5+", new string[] { "Go", "mi", "sol" } },
            { "Gm6", new string[] { "Go", "mi", "sol" } },
            { "Gm7", new string[] { "Go", "mi", "sol" } },
            { "Gm7M", new string[] { "Go", "mi", "sol" } },
            { "G°", new string[] { "Go", "mi", "sol" } },

            { "G#", new string[] { "G#o", "mi", "sol" } },
            { "G#4", new string[] { "G#o", "mi", "sol" } },
            { "G#5+", new string[] { "G#o", "mi", "sol" } },
            { "G#6", new string[] { "G#o", "mi", "sol" } },
            { "G#7", new string[] { "G#o", "mi", "sol" } },
            { "G#7M", new string[] { "G#o", "mi", "sol" } },
            { "G#9", new string[] { "G#o", "mi", "sol" } },
            { "G#m", new string[] { "G#o", "mi", "sol" } },
            { "G#m5+", new string[] { "G#o", "mi", "sol" } },
            { "G#m6", new string[] { "G#o", "mi", "sol" } },
            { "G#m7", new string[] { "G#o", "mi", "sol" } },
            { "G#m7M", new string[] { "G#o", "mi", "sol" } },
            { "G#°", new string[] { "G#o", "mi", "sol" } },

            { "A", new string[] { "Ao", "mi", "sol" } },
            { "A4", new string[] { "Ao", "mi", "sol" } },
            { "A5+", new string[] { "Ao", "mi", "sol" } },
            { "A6", new string[] { "Ao", "mi", "sol" } },
            { "A7", new string[] { "Ao", "mi", "sol" } },
            { "A7M", new string[] { "Ao", "mi", "sol" } },
            { "A9", new string[] { "Ao", "mi", "sol" } },
            { "Am", new string[] { "Ao", "mi", "sol" } },
            { "Am5+", new string[] { "Ao", "mi", "sol" } },
            { "Am6", new string[] { "Ao", "mi", "sol" } },
            { "Am7", new string[] { "Ao", "mi", "sol" } },
            { "Am7M", new string[] { "Ao", "mi", "sol" } },
            { "A°", new string[] { "Ao", "mi", "sol" } },

            { "A#", new string[] { "A#o", "mi", "sol" } },
            { "A#4", new string[] { "A#o", "mi", "sol" } },
            { "A#5+", new string[] { "A#o", "mi", "sol" } },
            { "A#6", new string[] { "A#o", "mi", "sol" } },
            { "A#7", new string[] { "A#o", "mi", "sol" } },
            { "A#7M", new string[] { "A#o", "mi", "sol" } },
            { "A#9", new string[] { "A#o", "mi", "sol" } },
            { "A#m", new string[] { "A#o", "mi", "sol" } },
            { "A#m5+", new string[] { "A#o", "mi", "sol" } },
            { "A#m6", new string[] { "A#o", "mi", "sol" } },
            { "A#m7", new string[] { "A#o", "mi", "sol" } },
            { "A#m7M", new string[] { "A#o", "mi", "sol" } },
            { "A#°", new string[] { "A#o", "mi", "sol" } },

            { "B", new string[] { "Bo", "mi", "sol" } },
            { "B4", new string[] { "Bo", "mi", "sol" } },
            { "B5+", new string[] { "Bo", "mi", "sol" } },
            { "B6", new string[] { "Bo", "mi", "sol" } },
            { "B7", new string[] { "Bo", "mi", "sol" } },
            { "B7M", new string[] { "Bo", "mi", "sol" } },
            { "B9", new string[] { "Bo", "mi", "sol" } },
            { "Bm", new string[] { "Bo", "mi", "sol" } },
            { "Bm5+", new string[] { "Bo", "mi", "sol" } },
            { "Bm6", new string[] { "Bo", "mi", "sol" } },
            { "Bm7", new string[] { "Bo", "mi", "sol" } },
            { "Bm7M", new string[] { "Bo", "mi", "sol" } },
            { "B°", new string[] { "Bo", "mi", "sol" } },
        };
    }
}
