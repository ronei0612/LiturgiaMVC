﻿using System.Globalization;
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
                    "orgao_Dm_mao, https://docs.google.com/uc?export=download&id=12cpLodPOo4y6aQjHEt6X-UOuEy4WAgq1" + Environment.NewLine +
                    "orgao_D#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_E_mao, https://docs.google.com/uc?export=download&id=1hgYT3kAd2iRSbcB1x9Y-b3vOsQW3_F_8" + Environment.NewLine +
                    "orgao_Em_mao, https://docs.google.com/uc?export=download&id=13ndZEZkqxn_FemSfbp-dbNoxULzLu0jB" + Environment.NewLine +
                    "orgao_F_mao, https://docs.google.com/uc?export=download&id=17u3T29WtVPKWgfa6VDcmmwawiR9T8vVp" + Environment.NewLine +
                    "orgao_Fm_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G_mao, https://docs.google.com/uc?export=download&id=1D_nGx2_EBMSqdAyB3RnN04IWCgctOybe" + Environment.NewLine +
                    "orgao_Gm_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A_mao, https://docs.google.com/uc?export=download&id=1c8ZnfDMeQJbdjTm-W-CXk9eq1HGNne2o" + Environment.NewLine +
                    "orgao_Am_mao, https://docs.google.com/uc?export=download&id=10EFptu5tsdhqzCZBWnVTYIy-tU7LoTw2" + Environment.NewLine +
                    "orgao_A#_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A#m_mao, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_Bb_mao, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_B_mao, https://docs.google.com/uc?export=download&id=1jwX3WuxcBNLSV2lkrW6UHpjtO9qtpFAa" + Environment.NewLine +
                    "orgao_Bm_mao, https://docs.google.com/uc?export=download&id=1xgdJALMsRfea6eOsEVNuBlTupabp4JdD" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_solo, https://docs.google.com/uc?export=download&id=1lCOqdc5FZ-f_1yD1AaH7AN_K0YeBYrPy" + Environment.NewLine +
                    "orgao_Cm_solo, https://docs.google.com/uc?export=download&id=1P_pGNapihJdTilsBo1y8ija0Afm7m5_3" + Environment.NewLine +
                    "orgao_C#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_C#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D_solo, https://docs.google.com/uc?export=download&id=1jmFB7vxCH5_1b6Ym0aJXK1PGagwRgBnS" + Environment.NewLine +
                    "orgao_Dm_solo, https://docs.google.com/uc?export=download&id=1XmCNrr14EvDi0ot1oaasP0KMWafuIGo7" + Environment.NewLine +
                    "orgao_D#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_E_solo, https://docs.google.com/uc?export=download&id=1hgYT3kAd2iRSbcB1x9Y-b3vOsQW3_F_8" + Environment.NewLine +
                    "orgao_Em_solo, https://docs.google.com/uc?export=download&id=1RLCW9EPiyaMAdPhNdT2Gdq3xGshpa1rU" + Environment.NewLine +
                    "orgao_F_solo, https://docs.google.com/uc?export=download&id=13PDqSp0k38G8fGxOMHzNm0KLJBdHLk6a" + Environment.NewLine +
                    "orgao_Fm_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G_solo, https://docs.google.com/uc?export=download&id=17K5HwfHIqLhhrJDC030KXgiPCOKudUkj" + Environment.NewLine +
                    "orgao_Gm_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#m_solo, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A_solo, https://docs.google.com/uc?export=download&id=1c8ZnfDMeQJbdjTm-W-CXk9eq1HGNne2o" + Environment.NewLine +
                    "orgao_Am_solo, https://docs.google.com/uc?export=download&id=1EFnruYEFZdU16S9AGJbKDwtJm8AByP6G" + Environment.NewLine +
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
                    "orgao_Dm_full, https://docs.google.com/uc?export=download&id=1PpBiLF35vj9VPSGbN_1x24mcQ9TAQiwc" + Environment.NewLine +
                    "orgao_D#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_D#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_E_full, https://docs.google.com/uc?export=download&id=1hgYT3kAd2iRSbcB1x9Y-b3vOsQW3_F_8" + Environment.NewLine +
                    "orgao_Em_full, https://docs.google.com/uc?export=download&id=1zLia9ZZqvJ0N3BfVxVof91JMmMDJUDls" + Environment.NewLine +
                    "orgao_F_full, https://docs.google.com/uc?export=download&id=1Ox2QLpimWApu3HEFGmGK3eRt8Cra-Az4" + Environment.NewLine +
                    "orgao_Fm_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_F#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G_full, https://docs.google.com/uc?export=download&id=1Bpd4tkh2dmCrymIpgOQm75vd-vbF4-CK" + Environment.NewLine +
                    "orgao_Gm_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_G#m_full, https://docs.google.com/uc?export=download&id=1ZYNZotCi_OJ2Bs-ot4FGk0eR4zsWuw1W" + Environment.NewLine +
                    "orgao_A_full, https://docs.google.com/uc?export=download&id=1c8ZnfDMeQJbdjTm-W-CXk9eq1HGNne2o" + Environment.NewLine +
                    "orgao_Am_full, https://docs.google.com/uc?export=download&id=1ZYJnfJHnSzqgPHbTnn2UqKwMBMfTA6KR" + Environment.NewLine +
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
                        acordesLinksDict.Add(linha.Split(',')[0], linha.Split(',')[1]);

            Variaveis.acordesLinks = acordesLinksDict;
        }
    }
}
