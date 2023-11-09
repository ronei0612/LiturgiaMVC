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
                            tonsMaiores.Add(item.Trim());
                            tonsMenores.Add(Variaveis.acordesMenoresRelativos[item.Trim()]);
                        }
                    else
                    {
                        tonsMaiores.Add(linha.Trim());
                        tonsMenores.Add(Variaveis.acordesMenoresRelativos[linha.Trim()]);
                    }
                }

            Variaveis.tonsMaiores = tonsMaiores.ToArray();
            Variaveis.tonsMenores = tonsMenores.ToArray();
        }

        public static void LerArquivoAcordesLinks_old()
        {
            var acordesLinksDict = new Dictionary<string, string>();

            if (File.Exists(Variaveis.arquivoAcordesLinks) == false) {
                File.WriteAllText(Variaveis.arquivoAcordesLinks,
                    "orgao_C_mao, ./Sons/orgao 1BWIg8AHI21WzimmfK2RfB7ZM9SCzM06G" + Environment.NewLine +
                    "orgao_Cm_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_C#_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_C#m_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_D_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_Dm_mao, ./Sons/orgao 12cpLodPOo4y6aQjHEt6X-UOuEy4WAgq1" + Environment.NewLine +
                    "orgao_D#_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_D#m_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_E_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_Em_mao, ./Sons/orgao 13ndZEZkqxn_FemSfbp-dbNoxULzLu0jB" + Environment.NewLine +
                    "orgao_F_mao, ./Sons/orgao 17u3T29WtVPKWgfa6VDcmmwawiR9T8vVp" + Environment.NewLine +
                    "orgao_Fm_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_F#_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_F#m_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_G_mao, ./Sons/orgao 1D_nGx2_EBMSqdAyB3RnN04IWCgctOybe" + Environment.NewLine +
                    "orgao_Gm_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_G#_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_G#m_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_A_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_Am_mao, ./Sons/orgao 10EFptu5tsdhqzCZBWnVTYIy-tU7LoTw2" + Environment.NewLine +
                    "orgao_A#_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_A#m_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_Bb_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_B_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_Bm_mao, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Cm_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_C#_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_C#m_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_D_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Dm_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_D#_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_D#m_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_E_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Em_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_F_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Fm_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_F#_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_F#m_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_G_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Gm_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_G#_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_G#m_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_A_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Am_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_A#_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_A#m_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Bb_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_B_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    "orgao_Bm_baixo, ./Sons/orgao baixo.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_full, ./Sons/orgao .ogg" + Environment.NewLine +
                    "orgao_Cm_full, ./Sons/orgao .ogg" + Environment.NewLine +
                    "orgao_C#_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_C#m_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_D_full, ./Sons/orgao .ogg" + Environment.NewLine +
                    "orgao_Dm_full, ./Sons/orgao 1PpBiLF35vj9VPSGbN_1x24mcQ9TAQiwc" + Environment.NewLine +
                    "orgao_D#_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_D#m_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_E_full, ./Sons/orgao .ogg" + Environment.NewLine +
                    "orgao_Em_full, ./Sons/orgao 1zLia9ZZqvJ0N3BfVxVof91JMmMDJUDls" + Environment.NewLine +
                    "orgao_F_full, ./Sons/orgao 1Ox2QLpimWApu3HEFGmGK3eRt8Cra-Az4" + Environment.NewLine +
                    "orgao_Fm_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_F#_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_F#m_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_G_full, ./Sons/orgao 1Bpd4tkh2dmCrymIpgOQm75vd-vbF4-CK" + Environment.NewLine +
                    "orgao_Gm_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_G#_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_G#m_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_A_full, ./Sons/orgao .ogg" + Environment.NewLine +
                    "orgao_Am_full, ./Sons/orgao 1ZYJnfJHnSzqgPHbTnn2UqKwMBMfTA6KR" + Environment.NewLine +
                    "orgao_A#_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_A#m_full, ./Sons/orgao mao.ogg" + Environment.NewLine +
                    "orgao_Bb_full, ./Sons/orgao .ogg" + Environment.NewLine +
                    "orgao_B_full, ./Sons/orgao .ogg" + Environment.NewLine +
                    "orgao_Bm_full, ./Sons/orgao .ogg"
                );
            }

            var linhas = File.ReadAllLines(Variaveis.arquivoAcordesLinks);

            foreach (var linha in linhas)
                if (string.IsNullOrEmpty(linha) == false)
                    if (linha.Contains(','))
                        acordesLinksDict.Add(linha.Split(',')[0].Trim(), linha.Split(',')[1].Trim());

            Variaveis.acordesLinks = acordesLinksDict;
        }

        public static void LerArquivoAcordesLinks()
        {
            var acordesLinksDict = new Dictionary<string, string>();

            if (File.Exists(Variaveis.arquivoAcordesLinks) == false) {
                File.WriteAllText(Variaveis.arquivoAcordesLinks,
                    "orgao_C_mao, ./Sons/orgao C mao.ogg" + Environment.NewLine +
                    "orgao_Cm_mao, ./Sons/orgao Cm mao.ogg" + Environment.NewLine +
                    "orgao_C#_mao, ./Sons/orgao C# mao.ogg" + Environment.NewLine +
                    "orgao_C#m_mao, ./Sons/orgao C#m mao.ogg" + Environment.NewLine +
                    "orgao_D_mao, ./Sons/orgao D mao.ogg" + Environment.NewLine +
                    "orgao_Dm_mao, ./Sons/orgao Dm mao.ogg" + Environment.NewLine +
                    "orgao_D#_mao, ./Sons/orgao D# mao.ogg" + Environment.NewLine +
                    "orgao_D#m_mao, ./Sons/orgao D#m mao.ogg" + Environment.NewLine +
                    "orgao_E_mao, ./Sons/orgao E mao.ogg" + Environment.NewLine +
                    "orgao_Em_mao, ./Sons/orgao Em mao.ogg" + Environment.NewLine +
                    "orgao_F_mao, ./Sons/orgao F mao.ogg" + Environment.NewLine +
                    "orgao_Fm_mao, ./Sons/orgao F# mao.ogg" + Environment.NewLine +
                    "orgao_F#_mao, ./Sons/orgao F# mao.ogg" + Environment.NewLine +
                    "orgao_F#m_mao, ./Sons/orgao F#m mao.ogg" + Environment.NewLine +
                    "orgao_G_mao, ./Sons/orgao G mao.ogg" + Environment.NewLine +
                    "orgao_Gm_mao, ./Sons/orgao Gm mao.ogg" + Environment.NewLine +
                    "orgao_G#_mao, ./Sons/orgao G# mao.ogg" + Environment.NewLine +
                    "orgao_G#m_mao, ./Sons/orgao G#m mao.ogg" + Environment.NewLine +
                    "orgao_A_mao, ./Sons/orgao A mao.ogg" + Environment.NewLine +
                    "orgao_Am_mao, ./Sons/orgao Am mao.ogg" + Environment.NewLine +
                    "orgao_A#_mao, ./Sons/orgao A# mao.ogg" + Environment.NewLine +
                    "orgao_A#m_mao, ./Sons/orgao A#m mao.ogg" + Environment.NewLine +
                    "orgao_Bb_mao, ./Sons/orgao Bb mao.ogg" + Environment.NewLine +
                    "orgao_B_mao, ./Sons/orgao B mao.ogg" + Environment.NewLine +
                    "orgao_Bm_mao, ./Sons/orgao Bm mao.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_baixo, ./Sons/orgao C baixo.ogg" + Environment.NewLine +
                    "orgao_Cm_baixo, ./Sons/orgao C baixo.ogg" + Environment.NewLine +
                    "orgao_C#_baixo, ./Sons/orgao C# baixo.ogg" + Environment.NewLine +
                    "orgao_C#m_baixo, ./Sons/orgao C# baixo.ogg" + Environment.NewLine +
                    "orgao_D_baixo, ./Sons/orgao D baixo.ogg" + Environment.NewLine +
                    "orgao_Dm_baixo, ./Sons/orgao D baixo.ogg" + Environment.NewLine +
                    "orgao_D#_baixo, ./Sons/orgao D# baixo.ogg" + Environment.NewLine +
                    "orgao_D#m_baixo, ./Sons/orgao D# baixo.ogg" + Environment.NewLine +
                    "orgao_E_baixo, ./Sons/orgao E baixo.ogg" + Environment.NewLine +
                    "orgao_Em_baixo, ./Sons/orgao E baixo.ogg" + Environment.NewLine +
                    "orgao_F_baixo, ./Sons/orgao F baixo.ogg" + Environment.NewLine +
                    "orgao_Fm_baixo, ./Sons/orgao F baixo.ogg" + Environment.NewLine +
                    "orgao_F#_baixo, ./Sons/orgao F# baixo.ogg" + Environment.NewLine +
                    "orgao_F#m_baixo, ./Sons/orgao F# baixo.ogg" + Environment.NewLine +
                    "orgao_G_baixo, ./Sons/orgao G baixo.ogg" + Environment.NewLine +
                    "orgao_Gm_baixo, ./Sons/orgao G baixo.ogg" + Environment.NewLine +
                    "orgao_G#_baixo, ./Sons/orgao G# baixo.ogg" + Environment.NewLine +
                    "orgao_G#m_baixo, ./Sons/orgao G# baixo.ogg" + Environment.NewLine +
                    "orgao_A_baixo, ./Sons/orgao A baixo.ogg" + Environment.NewLine +
                    "orgao_Am_baixo, ./Sons/orgao A baixo.ogg" + Environment.NewLine +
                    "orgao_A#_baixo, ./Sons/orgao A# baixo.ogg" + Environment.NewLine +
                    "orgao_A#m_baixo, ./Sons/orgao A# baixo.ogg" + Environment.NewLine +
                    "orgao_Bb_baixo, ./Sons/orgao Bb baixo.ogg" + Environment.NewLine +
                    "orgao_B_baixo, ./Sons/orgao B baixo.ogg" + Environment.NewLine +
                    "orgao_Bm_baixo, ./Sons/orgao B baixo.ogg"
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
