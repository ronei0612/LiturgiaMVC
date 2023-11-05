namespace LiturgiaMVC
{
    public class Variaveis
    {
        public static string pastaArquivos = "Arquivos";

        public static string links = "Links.txt";

        public static string IPs = "IPs.csv";

        public static string arquivoLinks = Path.Combine(pastaArquivos, links);

        public static string arquivoIPs = Path.Combine(pastaArquivos, IPs);

        public static string[] tonsMaiores = { "C", "D", "E", "F", "G", "A", "B" };

        public static string[] tonsMenores = { "Am", "Bm", "Cm", "Dm", "Em", "Fm", "Gm" };

        public static string[] acidentes = { "5+", "6", "7", "7+", "9", "11" };
    }
}
