using System.Globalization;
using System.Text.RegularExpressions;

namespace LiturgiaMVC
{
    public class Ferramentas
    {
        public static Dictionary<string, string> LerArquivoLinks()
        {
            var linksDict = new Dictionary<string, string>();

            if (Directory.Exists(Variaveis.pastaArquivos) == false)
                Directory.CreateDirectory(Variaveis.pastaArquivos);

            if (File.Exists(Variaveis.arquivoLinks) == false)
            {
                File.WriteAllText(Variaveis.arquivoLinks, "imagem = link");
                return linksDict;
            }

            if (File.ReadAllText(Variaveis.arquivoLinks) == "imagem = link")
                return linksDict;

            if (File.ReadAllText(Variaveis.arquivoLinks).Contains('=') == false)
                return linksDict;

            var linhas = File.ReadAllLines(Variaveis.arquivoLinks);

            foreach (var linha in linhas)
            {
                if (string.IsNullOrEmpty(linha) == false)
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
            }

            return linksDict;
        }

        public static void EscreverArquivoLinks(string dados)
        {
            File.WriteAllText(Variaveis.arquivoLinks, dados);
        }

        public static void EscreverInfoCliente(HttpContext httpContext)
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString();
            var dataHora = DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            var host = httpContext.Request.Host.Value;
            var path = httpContext.Request.Path.Value;

            File.AppendAllText(Variaveis.arquivoIPs, Environment.NewLine + ip + ";" + host + path + ";" + dataHora);
        }

        public static readonly Dictionary<string, string[]> acordes = new()
        {
            { "C", new string[] { "C", "Am", "F", "Dm", "G", "Em" } },
            { "D", new string[] { "D", "Bm", "G", "Em", "A", "F#m" } }
        };

        public static readonly Dictionary<string, string> acordesLinks = new()
        {
            { "orgaoC", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoCm", "https://docs.google.com/uc?export=download&id=1gbyBBLDo8CPoNPYBBnk-Y_6BfwejabHS" },
            { "orgaoC#", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoC#m", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoD", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoDm", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoD#", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoD#m", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoE", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoEm", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoF", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoFm", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoF#m", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoG", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoGm", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoG#", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoG#m", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoA", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoAm", "https://docs.google.com/uc?export=download&id=1gbyBBLDo8CPoNPYBBnk-Y_6BfwejabHS" },
            { "orgaoA#", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },            
            { "orgaoA#m", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoB", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoBm", "https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
        };
    }
}
