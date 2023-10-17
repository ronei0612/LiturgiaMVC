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
            { "C", new string[] { "C", "Dm", "Em", "F", "G", "Am", "", "" } },
            { "D", new string[] { "D", "Em", "F#m", "G", "A", "Bm", "", "" } },
            { "E", new string[] { "E", "F#m", "G#m", "A", "B", "C#m", "", "" } },
            { "F", new string[] { "F", "Gm", "Am", "Bb", "C", "Dm" , "", ""} },
            { "G", new string[] { "G", "Am", "Bm", "C", "D", "Em" , "", ""} },
            { "A", new string[] { "A", "Bm", "C#m", "D", "E", "F#m" , "", ""} },
            { "Am", new string[] { "C", "Dm", "Em", "F", "G", "Am", "A", "E" } },
            { "Bm", new string[] { "D", "Em", "F#m", "G", "A", "Bm", "B", "F#" } },
            { "C#m", new string[] { "E", "F#m", "G#m", "A", "B", "Cm", "C#", "G#" } },
            { "Dm", new string[] { "F", "Gm", "Am", "Bb", "C", "Dm", "D", "A" } },
            { "Em", new string[] { "G", "Am", "Bm", "C", "D", "Em", "E", "B" } },
            { "F#m", new string[] { "A", "Bm", "C#m", "D", "E", "F#m", "F#", "C#" } },
        };

        public static readonly Dictionary<string, string> acordesLinks = new()
        {
            { "orgaoC", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoCm", "https://docs.google.com/uc?export=download&id=" + "1P_pGNapihJdTilsBo1y8ija0Afm7m5_3" },
            { "orgaoC#", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoC#m", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoD", "https://docs.google.com/uc?export=download&id=" + "1jmFB7vxCH5_1b6Ym0aJXK1PGagwRgBnS" },
            { "orgaoDm", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoD#", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoD#m", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoE", "https://docs.google.com/uc?export=download&id=" + "1hgYT3kAd2iRSbcB1x9Y-b3vOsQW3_F_8" },
            { "orgaoEm", "https://docs.google.com/uc?export=download&id=" + "1HW8GU7sVt3NaqEZPoAkQZz64cBoRYiUy" },
            { "orgaoF", "https://docs.google.com/uc?export=download&id=" + "17lkJE9a-JwPKAXg-6BCSHf9gZtxn6X_x" },
            { "orgaoFm", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoF#", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoF#m", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoG", "https://docs.google.com/uc?export=download&id=" + "1NqmQ-R37hLCP0RMuHeGU2EeVnZnnll6E" },
            { "orgaoGm", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoG#", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoG#m", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoA", "https://docs.google.com/uc?export=download&id=" + "1c8ZnfDMeQJbdjTm-W-CXk9eq1HGNne2o" },
            { "orgaoAm", "https://docs.google.com/uc?export=download&id=" + "1gbyBBLDo8CPoNPYBBnk-Y_6BfwejabHS" },
            { "orgaoA#", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },            
            { "orgaoA#m", "https://docs.google.com/uc?export=download&id=" + "1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" },
            { "orgaoB", "https://docs.google.com/uc?export=download&id=" + "1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" },
            { "orgaoBm", "https://docs.google.com/uc?export=download&id=" + "1xgdJALMsRfea6eOsEVNuBlTupabp4JdD" },
        };
    }
}
