namespace LiturgiaMVC
{
    public class Variaveis
    {
        public static string pastaArquivos = "Arquivos";
        public static string pastaSons = "./Sons/";
        public static string pastaSonsOrgao = "./Sons/Orgao/";
        public static string pastaSonsStrings = "./Sons/Strings/";
		public static string pastaSonsPercussao = "./Sons/Percussao/";

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
            { "Cb", "B" },
            { "C", "C" },
            { "C#", "C#" },
            { "Db", "C#" },
            { "D", "D" },
            { "D#", "D#" },
            { "Eb", "D#" },
            { "E", "E" },
            { "E#", "F" },
            { "Fb", "E" },
            { "F", "F" },
            { "F#", "F#" },
            { "Gb", "F#" },
            { "G", "G" },
            { "G#", "G#" },
            { "Ab", "G#" },
            { "A", "A" },
            { "A#", "A#" },
            { "Bb", "A#" },
            { "B", "B" },
            { "B#", "C" }
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
            { "C", new string[] { "c", "e", "g" } },
            { "C4", new string[] { "c", "f", "g" } },
            { "C5+", new string[] { "c", "e", "g#" } },
            { "C6", new string[] { "c", "e", "g", "a" } },
            { "C7", new string[] { "c", "e", "g", "a#" } },
            { "C7M", new string[] { "c", "e", "g", "b" } },
            { "C9", new string[] { "c", "e", "g", "d" } },
            { "C°", new string[] { "c", "d#", "f#" } },
            { "C°7", new string[] { "c", "d#", "f#", "a#" } },
            { "Cm", new string[] { "c", "d#", "g" } },
            { "Cm5+", new string[] { "c", "d#", "g#" } },
            { "Cm6", new string[] { "c", "d#", "g", "a" } },
            { "Cm7", new string[] { "c", "d#", "g", "a#" } },
            { "Cm7M", new string[] { "c", "d#", "g", "b" } },

            { "C#", new string[] { "c#", "f", "g#" } },
            { "C#4", new string[] { "c#", "f#", "g#" } },
            { "C#5+", new string[] { "c#", "f", "a" } },
            { "C#6", new string[] { "c#", "f", "g#", "a#" } },
            { "C#7", new string[] { "c#", "f", "g#", "b" } },
            { "C#7M", new string[] { "c#", "f", "g#", "c" } },
            { "C#9", new string[] { "c#", "f", "g#", "d#" } },
            { "C#°", new string[] { "c#", "e", "g" } },
            { "C#°7", new string[] { "c#", "e", "g", "b" } },
            { "C#m", new string[] { "c#", "e", "g#" } },
            { "C#m5+", new string[] { "c#", "e", "a" } },
            { "C#m6", new string[] { "c#", "e", "g#", "a#" } },
            { "C#m7", new string[] { "c#", "e", "g#", "b" } },
            { "C#m7M", new string[] { "c#", "e", "g#", "c" } },

            { "D", new string[] { "d", "f#", "a" } },
            { "D4", new string[] { "d", "g", "a" } },
            { "D5+", new string[] { "d", "f#", "a#" } },
            { "D6", new string[] { "d", "f#", "a", "b" } },
            { "D7", new string[] { "d", "f#", "a", "c" } },
            { "D7M", new string[] { "d", "f#", "a", "c#" } },
            { "D9", new string[] { "d", "f#", "a", "e" } },
            { "D°", new string[] { "d", "f", "g#" } },
            { "D°7", new string[] { "d", "f", "g#", "c" } },
            { "Dm", new string[] { "d", "f", "a" } },
            { "Dm5+", new string[] { "d", "f", "a#" } },
            { "Dm6", new string[] { "d", "f", "a", "b" } },
            { "Dm7", new string[] { "d", "f", "a", "c" } },
            { "Dm7M", new string[] { "d", "f", "a", "c#" } },

            { "D#", new string[] { "d#", "g", "a#" } },
            { "D#4", new string[] { "d#", "g#", "a#" } },
            { "D#5+", new string[] { "d#", "g", "b" } },
            { "D#6", new string[] { "d#", "g", "a#", "c" } },
            { "D#7", new string[] { "d#", "g", "a#", "c#" } },
            { "D#7M", new string[] { "d#", "g", "a#", "d" } },
            { "D#9", new string[] { "d#", "g", "a#", "f" } },
            { "D#°", new string[] { "d#", "f#", "a" } },
            { "D#°7", new string[] { "d#", "f#", "a", "c#" } },
            { "D#m", new string[] { "d#", "f#", "a#" } },
            { "D#m5+", new string[] { "d#", "f#", "b" } },
            { "D#m6", new string[] { "d#", "f#", "a#", "c" } },
            { "D#m7", new string[] { "d#", "f#", "a#", "c#" } },
            { "D#m7M", new string[] { "d#", "f#", "a#", "d" } },

            { "E", new string[] { "e", "g#", "b" } },
            { "E4", new string[] { "e", "a", "b" } },
            { "E5+", new string[] { "e", "g#", "c" } },
            { "E6", new string[] { "e", "g#", "b", "c#" } },
            { "E7", new string[] { "e", "g#", "b", "d" } },
            { "E7M", new string[] { "e", "g#", "b", "d#" } },
            { "E9", new string[] { "e", "g#", "b", "f#" } },
            { "E°", new string[] { "e", "g", "a#" } },
            { "E°7", new string[] { "e", "g", "a#", "d" } },
            { "Em", new string[] { "e", "g", "b" } },
            { "Em5+", new string[] { "e", "g", "c" } },
            { "Em6", new string[] { "e", "g", "b", "c#" } },
            { "Em7", new string[] { "e", "g", "b", "d" } },
            { "Em7M", new string[] { "e", "g", "b", "d#" } },

            { "F", new string[] { "f", "a", "c" } },
            { "F4", new string[] { "f", "a#", "c" } },
            { "F5+", new string[] { "f", "a", "c#" } },
            { "F6", new string[] { "f", "a", "c", "d" } },
            { "F7", new string[] { "f", "a", "c", "d#" } },
            { "F7M", new string[] { "f", "a", "c", "e" } },
            { "F9", new string[] { "f", "a", "c", "g" } },
            { "F°", new string[] { "f", "g#", "b" } },
            { "F°7", new string[] { "f", "g#", "b", "d#" } },
            { "Fm", new string[] { "f", "g#", "c" } },
            { "Fm5+", new string[] { "f", "g#", "c#" } },
            { "Fm6", new string[] { "f", "g#", "c", "d" } },
            { "Fm7", new string[] { "f", "g#", "c", "d#" } },
            { "Fm7M", new string[] { "f", "g#", "c", "e" } },

            { "F#", new string[] { "f#", "a#", "c#" } },
            { "F#4", new string[] { "f#", "b", "c#" } },
            { "F#5+", new string[] { "f#", "a#", "d" } },
            { "F#6", new string[] { "f#", "a#", "c#", "d#" } },
            { "F#7", new string[] { "f#", "a#", "c#", "e" } },
            { "F#7M", new string[] { "f#", "a#", "c#", "f" } },
            { "F#9", new string[] { "f#", "a#", "c#", "g#" } },
            { "F#°", new string[] { "f#", "a", "c" } },
            { "F#°7", new string[] { "f#", "a", "c", "e" } },
            { "F#m", new string[] { "f#", "a", "c#" } },
            { "F#m5+", new string[] { "f#", "a", "d" } },
            { "F#m6", new string[] { "f#", "a", "c#", "d#" } },
            { "F#m7", new string[] { "f#", "a", "c#", "e" } },
            { "F#m7M", new string[] { "f#", "a", "c#", "f" } },

            { "G", new string[] { "g", "b", "d" } },
            { "G4", new string[] { "g", "c", "d" } },
            { "G5+", new string[] { "g", "b", "d#" } },
            { "G6", new string[] { "g", "b", "d", "e" } },
            { "G7", new string[] { "g", "b", "d", "f" } },
            { "G7M", new string[] { "g", "b", "d", "f#" } },
            { "G9", new string[] { "g", "b", "d", "a" } },
            { "G°", new string[] { "g", "a#", "c#" } },
            { "G°7", new string[] { "g", "a#", "c#", "f" } },
            { "Gm", new string[] { "g", "a#", "d" } },
            { "Gm5+", new string[] { "g", "a#", "d#" } },
            { "Gm6", new string[] { "g", "a#", "d", "e" } },
            { "Gm7", new string[] { "g", "a#", "d", "f" } },
            { "Gm7M", new string[] { "g", "a#", "d", "f#" } },

            { "G#", new string[] { "g#", "c", "d#" } },
            { "G#4", new string[] { "g#", "c#", "d#" } },
            { "G#5+", new string[] { "g#", "c", "e" } },
            { "G#6", new string[] { "g#", "c", "d#", "f" } },
            { "G#7", new string[] { "g#", "c", "d#", "f#" } },
            { "G#7M", new string[] { "g#", "c", "d#", "g" } },
            { "G#9", new string[] { "g#", "c", "d#", "a#" } },
            { "G#°", new string[] { "g#", "b", "d" } },
            { "G#°7", new string[] { "g#", "b", "d", "f#" } },
            { "G#m", new string[] { "g#", "b", "d#" } },
            { "G#m5+", new string[] { "g#", "b", "e" } },
            { "G#m6", new string[] { "g#", "b", "d#", "f" } },
            { "G#m7", new string[] { "g#", "b", "d#", "f#" } },
            { "G#m7M", new string[] { "g#", "b", "d#", "g" } },

            { "A", new string[] { "a", "c#", "e" } },
            { "A4", new string[] { "a", "d", "e" } },
            { "A5+", new string[] { "a", "c#", "f" } },
            { "A6", new string[] { "a", "c#", "e", "f#" } },
            { "A7", new string[] { "a", "c#", "e", "g" } },
            { "A7M", new string[] { "a", "c#", "e", "g#" } },
            { "A9", new string[] { "a", "c#", "e", "b" } },
            { "A°", new string[] { "a", "c", "d#" } },
            { "A°7", new string[] { "a", "c", "d#", "g" } },
            { "Am", new string[] { "a", "c", "e" } },
            { "Am5+", new string[] { "a", "c", "f" } },
            { "Am6", new string[] { "a", "c", "e", "f#" } },
            { "Am7", new string[] { "a", "c", "e", "g" } },
            { "Am7M", new string[] { "a", "c", "e", "g#" } },

            { "A#", new string[] { "a#", "d", "f" } },
            { "A#4", new string[] { "a#", "d#", "f" } },
            { "A#5+", new string[] { "a#", "d", "f#" } },
            { "A#6", new string[] { "a#", "d", "f", "g" } },
            { "A#7", new string[] { "a#", "d", "f", "g#" } },
            { "A#7M", new string[] { "a#", "d", "f", "a" } },
            { "A#9", new string[] { "a#", "d", "f", "c" } },
            { "A#°", new string[] { "a#", "c#", "e" } },
            { "A#°7", new string[] { "a#", "c#", "e", "g#" } },
            { "A#m", new string[] { "a#", "c#", "f" } },
            { "A#m5+", new string[] { "a#", "c#", "f#" } },
            { "A#m6", new string[] { "a#", "c#", "f", "g" } },
            { "A#m7", new string[] { "a#", "c#", "f", "g#" } },
            { "A#m7M", new string[] { "a#", "c#", "f", "a" } },

            { "B", new string[] { "b", "d#", "f#" } },
            { "B4", new string[] { "b", "e", "f#" } },
            { "B5+", new string[] { "b", "d#", "g" } },
            { "B6", new string[] { "b", "d#", "f#", "g#" } },
            { "B7", new string[] { "b", "d#", "f#", "a" } },
            { "B7M", new string[] { "b", "d#", "f#", "a#" } },
            { "B9", new string[] { "b", "d#", "f#", "c#" } },
            { "B°", new string[] { "b", "d", "f" } },
            { "B°7", new string[] { "b", "d", "f", "a" } },
            { "Bm", new string[] { "b", "d", "f#" } },
            { "Bm5+", new string[] { "b", "d", "g" } },
            { "Bm6", new string[] { "b", "d", "f#", "g#" } },
            { "Bm7", new string[] { "b", "d", "f#", "a" } },
            { "Bm7M", new string[] { "b", "d", "f#", "a#" } },
        };
    }
}
