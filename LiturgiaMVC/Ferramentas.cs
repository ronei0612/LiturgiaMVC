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

        public static void LerArquivoAcordesLista()
        {
            var tonsMaiores = new List<string>();
            var tonsMenores = new List<string>();

            if (File.Exists(Variaveis.arquivoAcordesLista) == false)
                File.WriteAllText(Variaveis.arquivoAcordesLista, "C, G");

            var linhas = File.ReadAllLines(Variaveis.arquivoAcordesLista);

            foreach (var linha in linhas)
                if (string.IsNullOrEmpty(linha) == false)
                {
                    if (linha.Contains(','))
                        foreach (var item in linha.Split(','))
                        {
                            tonsMaiores.Add(item);
                            tonsMenores.Add(Variaveis.acordesMenoresRelativos[item.Trim()]);
                        }
                    else
                    {
                        tonsMaiores.Add(linha);
                        tonsMenores.Add(Variaveis.acordesMenoresRelativos[linha.Trim()]);
                    }
                }

            Variaveis.tonsMaiores = tonsMaiores.ToArray();
            Variaveis.tonsMenores = tonsMenores.ToArray();
        }

        public static void LerArquivoAcordesLinks()
        {
            var acordesLinksDict = new Dictionary<string, string>();

            if (File.Exists(Variaveis.arquivoAcordesLinks) == false) {
                File.WriteAllText(Variaveis.arquivoAcordesLinks,
                    "orgao_C_mao, https://docs.google.com/uc?export=download&id=1BWIg8AHI21WzimmfK2RfB7ZM9SCzM06G" + Environment.NewLine +
                    "orgao_Cm_mao, https://docs.google.com/uc?export=download&id=1P_pGNapihJdTilsBo1y8ija0Afm7m5_3" + Environment.NewLine +
                    "orgao_C#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_C#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D_mao, https://docs.google.com/uc?export=download&id=1jmFB7vxCH5_1b6Ym0aJXK1PGagwRgBnS" + Environment.NewLine +
                    "orgao_Dm_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_E_mao, https://docs.google.com/uc?export=download&id=1hgYT3kAd2iRSbcB1x9Y-b3vOsQW3_F_8" + Environment.NewLine +
                    "orgao_Em_mao, https://docs.google.com/uc?export=download&id=1HW8GU7sVt3NaqEZPoAkQZz64cBoRYiUy" + Environment.NewLine +
                    "orgao_F_mao, https://docs.google.com/uc?export=download&id=17lkJE9a-JwPKAXg-6BCSHf9gZtxn6X_x" + Environment.NewLine +
                    "orgao_Fm_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G_mao, https://docs.google.com/uc?export=download&id=1NqmQ-R37hLCP0RMuHeGU2EeVnZnnll6E" + Environment.NewLine +
                    "orgao_Gm_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A_mao, https://docs.google.com/uc?export=download&id=1c8ZnfDMeQJbdjTm-W-CXk9eq1HGNne2o" + Environment.NewLine +
                    "orgao_Am_mao, https://docs.google.com/uc?export=download&id=1gbyBBLDo8CPoNPYBBnk-Y_6BfwejabHS" + Environment.NewLine +
                    "orgao_A#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_Bb_mao, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_B_mao, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_Bm_mao, https://docs.google.com/uc?export=download&id=1xgdJALMsRfea6eOsEVNuBlTupabp4JdD" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_solo, https://docs.google.com/uc?export=download&id=1KUelyfI40QuCw4h-WgEPbAbPa414MZ01" + Environment.NewLine +
                    "orgao_Cm_solo, https://docs.google.com/uc?export=download&id=1P_pGNapihJdTilsBo1y8ija0Afm7m5_3" + Environment.NewLine +
                    "orgao_C#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_C#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D_solo, https://docs.google.com/uc?export=download&id=1jmFB7vxCH5_1b6Ym0aJXK1PGagwRgBnS" + Environment.NewLine +
                    "orgao_Dm_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_E_solo, https://docs.google.com/uc?export=download&id=1hgYT3kAd2iRSbcB1x9Y-b3vOsQW3_F_8" + Environment.NewLine +
                    "orgao_Em_solo, https://docs.google.com/uc?export=download&id=1HW8GU7sVt3NaqEZPoAkQZz64cBoRYiUy" + Environment.NewLine +
                    "orgao_F_solo, https://docs.google.com/uc?export=download&id=17lkJE9a-JwPKAXg-6BCSHf9gZtxn6X_x" + Environment.NewLine +
                    "orgao_Fm_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G_solo, https://docs.google.com/uc?export=download&id=1NqmQ-R37hLCP0RMuHeGU2EeVnZnnll6E" + Environment.NewLine +
                    "orgao_Gm_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A_solo, https://docs.google.com/uc?export=download&id=1c8ZnfDMeQJbdjTm-W-CXk9eq1HGNne2o" + Environment.NewLine +
                    "orgao_Am_solo, https://docs.google.com/uc?export=download&id=1gbyBBLDo8CPoNPYBBnk-Y_6BfwejabHS" + Environment.NewLine +
                    "orgao_A#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_Bb_solo, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_B_solo, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_Bm_solo, https://docs.google.com/uc?export=download&id=1xgdJALMsRfea6eOsEVNuBlTupabp4JdD" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_full, https://docs.google.com/uc?export=download&id=1lCOqdc5FZ-f_1yD1AaH7AN_K0YeBYrPy" + Environment.NewLine +
                    "orgao_Cm_full, https://docs.google.com/uc?export=download&id=1P_pGNapihJdTilsBo1y8ija0Afm7m5_3" + Environment.NewLine +
                    "orgao_C#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_C#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D_full, https://docs.google.com/uc?export=download&id=1jmFB7vxCH5_1b6Ym0aJXK1PGagwRgBnS" + Environment.NewLine +
                    "orgao_Dm_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_E_full, https://docs.google.com/uc?export=download&id=1hgYT3kAd2iRSbcB1x9Y-b3vOsQW3_F_8" + Environment.NewLine +
                    "orgao_Em_full, https://docs.google.com/uc?export=download&id=1HW8GU7sVt3NaqEZPoAkQZz64cBoRYiUy" + Environment.NewLine +
                    "orgao_F_full, https://docs.google.com/uc?export=download&id=17lkJE9a-JwPKAXg-6BCSHf9gZtxn6X_x" + Environment.NewLine +
                    "orgao_Fm_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G_full, https://docs.google.com/uc?export=download&id=1NqmQ-R37hLCP0RMuHeGU2EeVnZnnll6E" + Environment.NewLine +
                    "orgao_Gm_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A_full, https://docs.google.com/uc?export=download&id=1c8ZnfDMeQJbdjTm-W-CXk9eq1HGNne2o" + Environment.NewLine +
                    "orgao_Am_full, https://docs.google.com/uc?export=download&id=1gbyBBLDo8CPoNPYBBnk-Y_6BfwejabHS" + Environment.NewLine +
                    "orgao_A#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_Bb_full, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_B_full, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_Bm_full, https://docs.google.com/uc?export=download&id=1xgdJALMsRfea6eOsEVNuBlTupabp4JdD"
                );
            }

            var linhas = File.ReadAllLines(Variaveis.arquivoAcordesLinks);

            foreach (var linha in linhas)
                if (string.IsNullOrEmpty(linha) == false)
                    if (linha.Contains(','))
                        acordesLinksDict.Add(linha.Split(',')[0].Trim(), linha.Split(',')[1].Trim());

            Variaveis.acordesLinks = acordesLinksDict;
        }
    }
}
