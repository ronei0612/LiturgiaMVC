namespace LiturgiaMVC
{
    public class Variaveis
    {
        public static string pastaArquivos = "Arquivos";

        public static string links = "Links.txt";

        public static string IPs = "IPs.txt";

        public static string arquivoLinks = Path.Combine(pastaArquivos, links);

        public static string arquivoIPs = Path.Combine(pastaArquivos, IPs);
    }
}
