using System.Text.RegularExpressions;

namespace LiturgiaMVC
{
    public class Ferramentas
    {
        public static Dictionary<string, string> LerArquivoLinks()
        {
            if (Directory.Exists(Variaveis.pastaArquivos) == false)
                Directory.CreateDirectory(Variaveis.pastaArquivos);

            if (File.Exists(Variaveis.arquivoLinks) == false)
                File.WriteAllText(Variaveis.arquivoLinks, "imagem=link");

            string[] linhas = File.ReadAllLines(Variaveis.arquivoLinks);
            var linksDict = new Dictionary<string, string>();

            foreach (string linha in linhas)
            {
                if (linha[0] != '#')
                {
                    if (linha.Contains(" = "))
                    {
                        string imagem = Regex.Split(linha, " = ")[0];

                        if (imagem.Contains("http"))
                        {
                            string link = Regex.Split(linha, " = ")[1];

                            if (link.Contains("http"))
                            {
                                if (link.Contains('#'))
                                    link = link.Split('#')[0];

                                if (imagem.Contains('#'))
                                    imagem = imagem.Split('#')[0];

                                linksDict.Add(imagem.Trim(), link.Trim());
                            }
                        }
                    }
                }
            }

            return linksDict;
        }
    }
}
