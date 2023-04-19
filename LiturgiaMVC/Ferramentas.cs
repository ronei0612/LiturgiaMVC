namespace LiturgiaMVC
{
    public class Ferramentas
    {
        public static string[] LerArquivoLinks()
        {
            if (Directory.Exists(Variaveis.pastaArquivos) == false)
                Directory.CreateDirectory(Variaveis.pastaArquivos);

            if (File.Exists(Variaveis.arquivoLinks) == false)
                File.WriteAllText(Variaveis.arquivoLinks, "hesed=" + Environment.NewLine + "hesedIMG=");

            return File.ReadAllLines(Variaveis.arquivoLinks);
        }
    }
}
