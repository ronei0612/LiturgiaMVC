using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            VerificarTamanhoMaximIps();

            var ip = httpContext.Connection.RemoteIpAddress?.ToString();
            var host = httpContext.Request.Host.Value;
            var path = httpContext.Request.Path.Value;

            var dataHoraBrasilia = DateTime.Now.AddHours(2);
            var dataHora = dataHoraBrasilia.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

            try
            {
                if (File.Exists(Variaveis.arquivoIPs) == false)
                    File.WriteAllText(Variaveis.arquivoIPs, "IP;EndPoint;UserAgent;Data e Hora");

                File.AppendAllText(Variaveis.arquivoIPs, Environment.NewLine + ip + ";" + host + path + ";" + userAgent + ";" + dataHora);
            }
            catch { }
        }

        static void VerificarTamanhoMaximIps()
        {
            if (File.Exists(Variaveis.arquivoIPs)) {
                var dezMegas = 1000000;
                var fileInfo = new FileInfo(Variaveis.arquivoIPs);

                if (fileInfo.Length > dezMegas)
                    File.Delete(Variaveis.arquivoIPs);
            }
        }

        public static void LerArquivoAcordesLista()
        {
            var tonsMaiores = new List<string>();
            var tonsMenores = new List<string>();

            if (File.Exists(Variaveis.arquivoAcordesLista) == false)
                File.WriteAllText(Variaveis.arquivoAcordesLista, Variaveis.tonsMaioresString);

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

        public static void LerArquivoNotasLinks()
        {
            if (File.Exists(Variaveis.arquivoNotasLinks) == false)
            {
                File.WriteAllText(Variaveis.arquivoNotasLinks,
                    "orgao_c, " + Variaveis.pastaSonsOrgao + "orgao do.ogg" + Environment.NewLine +
                    "orgao_c#, " + Variaveis.pastaSonsOrgao + "orgao do_.ogg" + Environment.NewLine +
                    "orgao_d, " + Variaveis.pastaSonsOrgao + "orgao re.ogg" + Environment.NewLine +
                    "orgao_d#, " + Variaveis.pastaSonsOrgao + "orgao re_.ogg" + Environment.NewLine +
                    "orgao_e, " + Variaveis.pastaSonsOrgao + "orgao mi.ogg" + Environment.NewLine +
                    "orgao_f, " + Variaveis.pastaSonsOrgao + "orgao fa.ogg" + Environment.NewLine +
                    "orgao_f#, " + Variaveis.pastaSonsOrgao + "orgao fa_.ogg" + Environment.NewLine +
                    "orgao_g, " + Variaveis.pastaSonsOrgao + "orgao sol.ogg" + Environment.NewLine +
                    "orgao_g#, " + Variaveis.pastaSonsOrgao + "orgao sol_.ogg" + Environment.NewLine +
                    "orgao_a, " + Variaveis.pastaSonsOrgao + "orgao la.ogg" + Environment.NewLine +
                    "orgao_a#, " + Variaveis.pastaSonsOrgao + "orgao la_.ogg" + Environment.NewLine +
                    "orgao_b, " + Variaveis.pastaSonsOrgao + "orgao si.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_c_baixo, " + Variaveis.pastaSonsOrgao + "orgao do baixo.ogg" + Environment.NewLine +
                    "orgao_c#_baixo, " + Variaveis.pastaSonsOrgao + "orgao do_ baixo.ogg" + Environment.NewLine +
                    "orgao_d_baixo, " + Variaveis.pastaSonsOrgao + "orgao re baixo.ogg" + Environment.NewLine +
                    "orgao_d#_baixo, " + Variaveis.pastaSonsOrgao + "orgao re_ baixo.ogg" + Environment.NewLine +
                    "orgao_e_baixo, " + Variaveis.pastaSonsOrgao + "orgao mi baixo.ogg" + Environment.NewLine +
                    "orgao_f_baixo, " + Variaveis.pastaSonsOrgao + "orgao fa baixo.ogg" + Environment.NewLine +
                    "orgao_f#_baixo, " + Variaveis.pastaSonsOrgao + "orgao fa_ baixo.ogg" + Environment.NewLine +
                    "orgao_g_baixo, " + Variaveis.pastaSonsOrgao + "orgao sol baixo.ogg" + Environment.NewLine +
                    "orgao_g#_baixo, " + Variaveis.pastaSonsOrgao + "orgao sol_ baixo.ogg" + Environment.NewLine +
                    "orgao_a_baixo, " + Variaveis.pastaSonsOrgao + "orgao la baixo.ogg" + Environment.NewLine +
                    "orgao_a#_baixo, " + Variaveis.pastaSonsOrgao + "orgao la_ baixo.ogg" + Environment.NewLine +
                    "orgao_b_baixo, " + Variaveis.pastaSonsOrgao + "orgao si baixo.ogg" +
                    Environment.NewLine +
                    "orgao_c_grave, " + Variaveis.pastaSonsOrgao + "orgao do grave.ogg" + Environment.NewLine +
                    "orgao_c#_grave, " + Variaveis.pastaSonsOrgao + "orgao do_ grave.ogg" + Environment.NewLine +
                    "orgao_d_grave, " + Variaveis.pastaSonsOrgao + "orgao re grave.ogg" + Environment.NewLine +
                    "orgao_d#_grave, " + Variaveis.pastaSonsOrgao + "orgao re_ grave.ogg" + Environment.NewLine +
                    "orgao_e_grave, " + Variaveis.pastaSonsOrgao + "orgao mi grave.ogg" + Environment.NewLine +
                    "orgao_f_grave, " + Variaveis.pastaSonsOrgao + "orgao fa grave.ogg" + Environment.NewLine +
                    "orgao_f#_grave, " + Variaveis.pastaSonsOrgao + "orgao fa_ grave.ogg" + Environment.NewLine +
                    "orgao_g_grave, " + Variaveis.pastaSonsOrgao + "orgao sol grave.ogg" + Environment.NewLine +
                    "orgao_g#_grave, " + Variaveis.pastaSonsOrgao + "orgao sol_ grave.ogg" + Environment.NewLine +
                    "orgao_a_grave, " + Variaveis.pastaSonsOrgao + "orgao la grave.ogg" + Environment.NewLine +
                    "orgao_a#_grave, " + Variaveis.pastaSonsOrgao + "orgao la_ grave.ogg" + Environment.NewLine +
                    "orgao_b_grave, " + Variaveis.pastaSonsOrgao + "orgao si grave.ogg" +
                    Environment.NewLine +
                    "epiano_c, " + Variaveis.pastaSonsOrgao + "epiano do.ogg" + Environment.NewLine +
                    "epiano_c#, " + Variaveis.pastaSonsOrgao + "epiano do_.ogg" + Environment.NewLine +
                    "epiano_d, " + Variaveis.pastaSonsOrgao + "epiano re.ogg" + Environment.NewLine +
                    "epiano_d#, " + Variaveis.pastaSonsOrgao + "epiano re_.ogg" + Environment.NewLine +
                    "epiano_e, " + Variaveis.pastaSonsOrgao + "epiano mi.ogg" + Environment.NewLine +
                    "epiano_f, " + Variaveis.pastaSonsOrgao + "epiano fa.ogg" + Environment.NewLine +
                    "epiano_f#, " + Variaveis.pastaSonsOrgao + "epiano fa_.ogg" + Environment.NewLine +
                    "epiano_g, " + Variaveis.pastaSonsOrgao + "epiano sol.ogg" + Environment.NewLine +
                    "epiano_g#, " + Variaveis.pastaSonsOrgao + "epiano sol_.ogg" + Environment.NewLine +
                    "epiano_a, " + Variaveis.pastaSonsOrgao + "epiano la.ogg" + Environment.NewLine +
                    "epiano_a#, " + Variaveis.pastaSonsOrgao + "epiano la_.ogg" + Environment.NewLine +
                    "epiano_b, " + Variaveis.pastaSonsOrgao + "epiano si.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "epiano_c_baixo, " + Variaveis.pastaSonsOrgao + "epiano do baixo.ogg" + Environment.NewLine +
                    "epiano_c#_baixo, " + Variaveis.pastaSonsOrgao + "epiano do_ baixo.ogg" + Environment.NewLine +
                    "epiano_d_baixo, " + Variaveis.pastaSonsOrgao + "epiano re baixo.ogg" + Environment.NewLine +
                    "epiano_d#_baixo, " + Variaveis.pastaSonsOrgao + "epiano re_ baixo.ogg" + Environment.NewLine +
                    "epiano_e_baixo, " + Variaveis.pastaSonsOrgao + "epiano mi baixo.ogg" + Environment.NewLine +
                    "epiano_f_baixo, " + Variaveis.pastaSonsOrgao + "epiano fa baixo.ogg" + Environment.NewLine +
                    "epiano_f#_baixo, " + Variaveis.pastaSonsOrgao + "epiano fa_ baixo.ogg" + Environment.NewLine +
                    "epiano_g_baixo, " + Variaveis.pastaSonsOrgao + "epiano sol baixo.ogg" + Environment.NewLine +
                    "epiano_g#_baixo, " + Variaveis.pastaSonsOrgao + "epiano sol_ baixo.ogg" + Environment.NewLine +
                    "epiano_a_baixo, " + Variaveis.pastaSonsOrgao + "epiano la baixo.ogg" + Environment.NewLine +
                    "epiano_a#_baixo, " + Variaveis.pastaSonsOrgao + "epiano la_ baixo.ogg" + Environment.NewLine +
                    "epiano_b_baixo, " + Variaveis.pastaSonsOrgao + "epiano si baixo.ogg" +
                    Environment.NewLine +
                    "epiano_c_grave, " + Variaveis.pastaSonsOrgao + "epiano do grave.ogg" + Environment.NewLine +
                    "epiano_c#_grave, " + Variaveis.pastaSonsOrgao + "epiano do_ grave.ogg" + Environment.NewLine +
                    "epiano_d_grave, " + Variaveis.pastaSonsOrgao + "epiano re grave.ogg" + Environment.NewLine +
                    "epiano_d#_grave, " + Variaveis.pastaSonsOrgao + "epiano re_ grave.ogg" + Environment.NewLine +
                    "epiano_e_grave, " + Variaveis.pastaSonsOrgao + "epiano mi grave.ogg" + Environment.NewLine +
                    "epiano_f_grave, " + Variaveis.pastaSonsOrgao + "epiano fa grave.ogg" + Environment.NewLine +
                    "epiano_f#_grave, " + Variaveis.pastaSonsOrgao + "epiano fa_ grave.ogg" + Environment.NewLine +
                    "epiano_g_grave, " + Variaveis.pastaSonsOrgao + "epiano sol grave.ogg" + Environment.NewLine +
                    "epiano_g#_grave, " + Variaveis.pastaSonsOrgao + "epiano sol_ grave.ogg" + Environment.NewLine +
                    "epiano_a_grave, " + Variaveis.pastaSonsOrgao + "epiano la grave.ogg" + Environment.NewLine +
                    "epiano_a#_grave, " + Variaveis.pastaSonsOrgao + "epiano la_ grave.ogg" + Environment.NewLine +
                    "epiano_b_grave, " + Variaveis.pastaSonsOrgao + "epiano si grave.ogg" +
                    Environment.NewLine +
                    "strings_c, " + Variaveis.pastaSonsStrings + "strings do.ogg" + Environment.NewLine +
                    "strings_c#, " + Variaveis.pastaSonsStrings + "strings do_.ogg" + Environment.NewLine +
                    "strings_d, " + Variaveis.pastaSonsStrings + "strings re.ogg" + Environment.NewLine +
                    "strings_d#, " + Variaveis.pastaSonsStrings + "strings re_.ogg" + Environment.NewLine +
                    "strings_e, " + Variaveis.pastaSonsStrings + "strings mi.ogg" + Environment.NewLine +
                    "strings_f, " + Variaveis.pastaSonsStrings + "strings fa.ogg" + Environment.NewLine +
                    "strings_f#, " + Variaveis.pastaSonsStrings + "strings fa_.ogg" + Environment.NewLine +
                    "strings_g, " + Variaveis.pastaSonsStrings + "strings sol.ogg" + Environment.NewLine +
                    "strings_g#, " + Variaveis.pastaSonsStrings + "strings sol_.ogg" + Environment.NewLine +
                    "strings_a, " + Variaveis.pastaSonsStrings + "strings la.ogg" + Environment.NewLine +
                    "strings_a#, " + Variaveis.pastaSonsStrings + "strings la_.ogg" + Environment.NewLine +
                    "strings_b, " + Variaveis.pastaSonsStrings + "strings si.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "strings_c_baixo, " + Variaveis.pastaSonsStrings + "strings do baixo.ogg" + Environment.NewLine +
                    "strings_c#_baixo, " + Variaveis.pastaSonsStrings + "strings do_ baixo.ogg" + Environment.NewLine +
                    "strings_d_baixo, " + Variaveis.pastaSonsStrings + "strings re baixo.ogg" + Environment.NewLine +
                    "strings_d#_baixo, " + Variaveis.pastaSonsStrings + "strings re_ baixo.ogg" + Environment.NewLine +
                    "strings_e_baixo, " + Variaveis.pastaSonsStrings + "strings mi baixo.ogg" + Environment.NewLine +
                    "strings_f_baixo, " + Variaveis.pastaSonsStrings + "strings fa baixo.ogg" + Environment.NewLine +
                    "strings_f#_baixo, " + Variaveis.pastaSonsStrings + "strings fa_ baixo.ogg" + Environment.NewLine +
                    "strings_g_baixo, " + Variaveis.pastaSonsStrings + "strings sol baixo.ogg" + Environment.NewLine +
                    "strings_g#_baixo, " + Variaveis.pastaSonsStrings + "strings sol_ baixo.ogg" + Environment.NewLine +
                    "strings_a_baixo, " + Variaveis.pastaSonsStrings + "strings la baixo.ogg" + Environment.NewLine +
                    "strings_a#_baixo, " + Variaveis.pastaSonsStrings + "strings la_ baixo.ogg" + Environment.NewLine +
                    "strings_b_baixo, " + Variaveis.pastaSonsStrings + "strings si baixo.ogg" + Environment.NewLine +
					Environment.NewLine +
					"chimes, " + Variaveis.pastaSonsPercussao + "chimes.ogg" + Environment.NewLine +
					"prato, " + Variaveis.pastaSonsPercussao + "prato.ogg" + Environment.NewLine
				);
            }

            var linhas = File.ReadAllLines(Variaveis.arquivoNotasLinks);
            var notasLinksDict = new Dictionary<string, string>();

            foreach (var linha in linhas)
                if (string.IsNullOrEmpty(linha) == false)
                    if (linha.Contains(','))
                        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", linha.Split(',')[1].Trim())))
                            notasLinksDict.Add(linha.Split(',')[0].Trim(), linha.Split(',')[1].Trim());
                        else
                            throw new ArgumentException("Arquivo não existe na pasta wwwroot: " + linha.Split(',')[1].Trim());

            Variaveis.notasLinks = notasLinksDict;
        }

        public static void LerArquivoAcordesLinks()
        {
            var acordesLinksDict = new Dictionary<string, string>();

            if (File.Exists(Variaveis.arquivoAcordesLinks) == false) {
                File.WriteAllText(Variaveis.arquivoAcordesLinks,
                    "orgao_C_mao, " + Variaveis.pastaSonsOrgao + "orgao C mao.ogg" + Environment.NewLine +
                    "orgao_Cm_mao, " + Variaveis.pastaSonsOrgao + "orgao Cm mao.ogg" + Environment.NewLine +
                    "orgao_C#_mao, " + Variaveis.pastaSonsOrgao + "orgao C_ mao.ogg" + Environment.NewLine +
                    "orgao_C#m_mao, " + Variaveis.pastaSonsOrgao + "orgao C_m mao.ogg" + Environment.NewLine +
                    "orgao_D_mao, " + Variaveis.pastaSonsOrgao + "orgao D mao.ogg" + Environment.NewLine +
                    "orgao_Dm_mao, " + Variaveis.pastaSonsOrgao + "orgao Dm mao.ogg" + Environment.NewLine +
                    "orgao_D#_mao, " + Variaveis.pastaSonsOrgao + "orgao D_ mao.ogg" + Environment.NewLine +
                    "orgao_D#m_mao, " + Variaveis.pastaSonsOrgao + "orgao D_m mao.ogg" + Environment.NewLine +
                    "orgao_E_mao, " + Variaveis.pastaSonsOrgao + "orgao E mao.ogg" + Environment.NewLine +
                    "orgao_Em_mao, " + Variaveis.pastaSonsOrgao + "orgao Em mao.ogg" + Environment.NewLine +
                    "orgao_Eb_mao, " + Variaveis.pastaSonsOrgao + "orgao D_ mao.ogg" + Environment.NewLine +
                    "orgao_Ebm_mao, " + Variaveis.pastaSonsOrgao + "orgao D_m mao.ogg" + Environment.NewLine +
                    "orgao_F_mao, " + Variaveis.pastaSonsOrgao + "orgao F mao.ogg" + Environment.NewLine +
                    "orgao_Fm_mao, " + Variaveis.pastaSonsOrgao + "orgao Fm mao.ogg" + Environment.NewLine +
                    "orgao_F#_mao, " + Variaveis.pastaSonsOrgao + "orgao F_ mao.ogg" + Environment.NewLine +
                    "orgao_F#m_mao, " + Variaveis.pastaSonsOrgao + "orgao F_m mao.ogg" + Environment.NewLine +
                    "orgao_G_mao, " + Variaveis.pastaSonsOrgao + "orgao G mao.ogg" + Environment.NewLine +
                    "orgao_Gm_mao, " + Variaveis.pastaSonsOrgao + "orgao Gm mao.ogg" + Environment.NewLine +
                    "orgao_G#_mao, " + Variaveis.pastaSonsOrgao + "orgao G_ mao.ogg" + Environment.NewLine +
                    "orgao_G#m_mao, " + Variaveis.pastaSonsOrgao + "orgao G_m mao.ogg" + Environment.NewLine +
                    "orgao_A_mao, " + Variaveis.pastaSonsOrgao + "orgao A mao.ogg" + Environment.NewLine +
                    "orgao_Am_mao, " + Variaveis.pastaSonsOrgao + "orgao Am mao.ogg" + Environment.NewLine +
                    "orgao_A#_mao, " + Variaveis.pastaSonsOrgao + "orgao A_ mao.ogg" + Environment.NewLine +
                    "orgao_A#m_mao, " + Variaveis.pastaSonsOrgao + "orgao A_m mao.ogg" + Environment.NewLine +
                    "orgao_B_mao, " + Variaveis.pastaSonsOrgao + "orgao B mao.ogg" + Environment.NewLine +
                    "orgao_Bm_mao, " + Variaveis.pastaSonsOrgao + "orgao Bm mao.ogg" + Environment.NewLine +
                    "orgao_Bb_mao, " + Variaveis.pastaSonsOrgao + "orgao A_ mao.ogg" + Environment.NewLine +
                    "orgao_Bbm_mao, " + Variaveis.pastaSonsOrgao + "orgao A_m mao.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_baixo, " + Variaveis.pastaSonsOrgao + "orgao C baixo.ogg" + Environment.NewLine +
                    "orgao_Cm_baixo, " + Variaveis.pastaSonsOrgao + "orgao C baixo.ogg" + Environment.NewLine +
                    "orgao_C#_baixo, " + Variaveis.pastaSonsOrgao + "orgao C_ baixo.ogg" + Environment.NewLine +
                    "orgao_C#m_baixo, " + Variaveis.pastaSonsOrgao + "orgao C_ baixo.ogg" + Environment.NewLine +
                    "orgao_D_baixo, " + Variaveis.pastaSonsOrgao + "orgao D baixo.ogg" + Environment.NewLine +
                    "orgao_Dm_baixo, " + Variaveis.pastaSonsOrgao + "orgao D baixo.ogg" + Environment.NewLine +
                    "orgao_D#_baixo, " + Variaveis.pastaSonsOrgao + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_D#m_baixo, " + Variaveis.pastaSonsOrgao + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_E_baixo, " + Variaveis.pastaSonsOrgao + "orgao E baixo.ogg" + Environment.NewLine +
                    "orgao_Em_baixo, " + Variaveis.pastaSonsOrgao + "orgao E baixo.ogg" + Environment.NewLine +
                    "orgao_Eb_baixo, " + Variaveis.pastaSonsOrgao + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_Ebm_baixo, " + Variaveis.pastaSonsOrgao + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_F_baixo, " + Variaveis.pastaSonsOrgao + "orgao F baixo.ogg" + Environment.NewLine +
                    "orgao_Fm_baixo, " + Variaveis.pastaSonsOrgao + "orgao F baixo.ogg" + Environment.NewLine +
                    "orgao_F#_baixo, " + Variaveis.pastaSonsOrgao + "orgao F_ baixo.ogg" + Environment.NewLine +
                    "orgao_F#m_baixo, " + Variaveis.pastaSonsOrgao + "orgao F_ baixo.ogg" + Environment.NewLine +
                    "orgao_G_baixo, " + Variaveis.pastaSonsOrgao + "orgao G baixo.ogg" + Environment.NewLine +
                    "orgao_Gm_baixo, " + Variaveis.pastaSonsOrgao + "orgao G baixo.ogg" + Environment.NewLine +
                    "orgao_G#_baixo, " + Variaveis.pastaSonsOrgao + "orgao G_ baixo.ogg" + Environment.NewLine +
                    "orgao_G#m_baixo, " + Variaveis.pastaSonsOrgao + "orgao G_ baixo.ogg" + Environment.NewLine +
                    "orgao_A_baixo, " + Variaveis.pastaSonsOrgao + "orgao A baixo.ogg" + Environment.NewLine +
                    "orgao_Am_baixo, " + Variaveis.pastaSonsOrgao + "orgao A baixo.ogg" + Environment.NewLine +
                    "orgao_A#_baixo, " + Variaveis.pastaSonsOrgao + "orgao A_ baixo.ogg" + Environment.NewLine +
                    "orgao_A#m_baixo, " + Variaveis.pastaSonsOrgao + "orgao A_ baixo.ogg" + Environment.NewLine +
                    "orgao_B_baixo, " + Variaveis.pastaSonsOrgao + "orgao B baixo.ogg" + Environment.NewLine +
                    "orgao_Bm_baixo, " + Variaveis.pastaSonsOrgao + "orgao B baixo.ogg" + Environment.NewLine +
                    "orgao_Bb_baixo, " + Variaveis.pastaSonsOrgao + "orgao A_ baixo.ogg" + Environment.NewLine +
                    "orgao_Bbm_baixo, " + Variaveis.pastaSonsOrgao + "orgao A_ baixo.ogg" +
                    Environment.NewLine +
                    "strings_C_mao, " + Variaveis.pastaSonsStrings + "strings C mao.ogg" + Environment.NewLine +
                    "strings_Cm_mao, " + Variaveis.pastaSonsStrings + "strings Cm mao.ogg" + Environment.NewLine +
                    "strings_C#_mao, " + Variaveis.pastaSonsStrings + "strings C_ mao.ogg" + Environment.NewLine +
                    "strings_C#m_mao, " + Variaveis.pastaSonsStrings + "strings C_m mao.ogg" + Environment.NewLine +
                    "strings_D_mao, " + Variaveis.pastaSonsStrings + "strings D mao.ogg" + Environment.NewLine +
                    "strings_Dm_mao, " + Variaveis.pastaSonsStrings + "strings Dm mao.ogg" + Environment.NewLine +
                    "strings_D#_mao, " + Variaveis.pastaSonsStrings + "strings D_ mao.ogg" + Environment.NewLine +
                    "strings_D#m_mao, " + Variaveis.pastaSonsStrings + "strings D_m mao.ogg" + Environment.NewLine +
                    "strings_E_mao, " + Variaveis.pastaSonsStrings + "strings E mao.ogg" + Environment.NewLine +
                    "strings_Em_mao, " + Variaveis.pastaSonsStrings + "strings Em mao.ogg" + Environment.NewLine +
                    "strings_Eb_mao, " + Variaveis.pastaSonsStrings + "strings D_ mao.ogg" + Environment.NewLine +
                    "strings_Ebm_mao, " + Variaveis.pastaSonsStrings + "strings D_m mao.ogg" + Environment.NewLine +
                    "strings_F_mao, " + Variaveis.pastaSonsStrings + "strings F mao.ogg" + Environment.NewLine +
                    "strings_Fm_mao, " + Variaveis.pastaSonsStrings + "strings Fm mao.ogg" + Environment.NewLine +
                    "strings_F#_mao, " + Variaveis.pastaSonsStrings + "strings F_ mao.ogg" + Environment.NewLine +
                    "strings_F#m_mao, " + Variaveis.pastaSonsStrings + "strings F_m mao.ogg" + Environment.NewLine +
                    "strings_G_mao, " + Variaveis.pastaSonsStrings + "strings G mao.ogg" + Environment.NewLine +
                    "strings_Gm_mao, " + Variaveis.pastaSonsStrings + "strings Gm mao.ogg" + Environment.NewLine +
                    "strings_G#_mao, " + Variaveis.pastaSonsStrings + "strings G_ mao.ogg" + Environment.NewLine +
                    "strings_G#m_mao, " + Variaveis.pastaSonsStrings + "strings G_m mao.ogg" + Environment.NewLine +
                    "strings_A_mao, " + Variaveis.pastaSonsStrings + "strings A mao.ogg" + Environment.NewLine +
                    "strings_Am_mao, " + Variaveis.pastaSonsStrings + "strings Am mao.ogg" + Environment.NewLine +
                    "strings_A#_mao, " + Variaveis.pastaSonsStrings + "strings A_ mao.ogg" + Environment.NewLine +
                    "strings_A#m_mao, " + Variaveis.pastaSonsStrings + "strings A_m mao.ogg" + Environment.NewLine +
                    "strings_B_mao, " + Variaveis.pastaSonsStrings + "strings B mao.ogg" + Environment.NewLine +
                    "strings_Bm_mao, " + Variaveis.pastaSonsStrings + "strings Bm mao.ogg" + Environment.NewLine +
                    "strings_Bb_mao, " + Variaveis.pastaSonsStrings + "strings A_ mao.ogg" + Environment.NewLine +
                    "strings_Bbm_mao, " + Variaveis.pastaSonsStrings + "strings A_m mao.ogg" + Environment.NewLine
                );
            }

            var linhas = File.ReadAllLines(Variaveis.arquivoAcordesLinks);

            foreach (var linha in linhas)
                if (string.IsNullOrEmpty(linha) == false)
                    if (linha.Contains(','))
                        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", linha.Split(',')[1].Trim())))
                            acordesLinksDict.Add(linha.Split(',')[0].Trim(), linha.Split(',')[1].Trim());
                        else
                            throw new ArgumentException("Arquivo não existe na pasta wwwroot: " + linha.Split(',')[1].Trim());

            Variaveis.acordesLinks = acordesLinksDict;
        }

        public static void LerArquivoRitmosBateria()
        {
            if (File.Exists(Variaveis.arquivoRitmosBateria) == false)
                File.WriteAllText(Variaveis.arquivoRitmosBateria, "{ \"Pop\": [13, 10], \"Country\": [15, 16] }");

            var texto = File.ReadAllText(Variaveis.arquivoRitmosBateria);
            texto = texto.Replace(Environment.NewLine, "").Replace(" ", "").Replace("],}", "]}");

            if (string.IsNullOrEmpty(texto))
                throw new ArgumentException("Arquivo de ritmos vazio: " + Variaveis.arquivoRitmosBateria);
            
            Variaveis.textoRitmos = texto;
        }

        public static void LerArquivoNotasAcordes()
        {
            if (File.Exists(Variaveis.arquivonotasAcordes) == false)
                File.WriteAllText(Variaveis.arquivonotasAcordes, ConvertToJson(Variaveis.notasAcordes, true));

            var texto = File.ReadAllText(Variaveis.arquivonotasAcordes).Replace(Environment.NewLine, "").Replace(@"\s+", "").Replace("],}", "]}");

            if (string.IsNullOrEmpty(texto))
                throw new ArgumentException("Arquivo de notas vazio: " + Variaveis.arquivonotasAcordes);
            
            Variaveis.textoNotasAcordes = texto;
        }

        public static string ConvertToJson(object obj, bool indentado = false)
        {
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            var json = JsonConvert.SerializeObject(obj, settings);

            if (indentado == false)
                json = json.Replace(Environment.NewLine, "").Replace(@"\s+", "").Replace("],}", "]}");

            return json;
        }

        public static JsonResult? CarregarVariaveis()
        {
            if (Variaveis.textoNotasAcordes == "")
                try
                {
                    Ferramentas.LerArquivoNotasAcordes();
                }
                catch (Exception ex)
                {
                    return new JsonResult("Error", new ErrorViewModel
                    {
                        Titulo = "Ler Arquivo das notas dos acordes",
                        Mensagem = ex.Message
                    });
                }

            if (Variaveis.acordesLinks == null)
                try {
                    Ferramentas.LerArquivoNotasLinks();
                }
                catch (Exception ex) {
                    return new JsonResult("Error", new ErrorViewModel {                    
                        Titulo = "Ler Arquivo dos links das notas",
                        Mensagem = ex.Message
                    });
                }

            if (Variaveis.tonsMaiores == null)
                try
                {
                    Ferramentas.LerArquivoAcordesLista();
                }
                catch (Exception ex)
                {
                    return new JsonResult("Error", new ErrorViewModel
                    {
                        Titulo = "Ler Arquivo da lista de acordes",
                        Mensagem = ex.Message
                    });
                }

            if (Variaveis.textoRitmos == "")
                try {
                    Ferramentas.LerArquivoRitmosBateria();
                }
                catch (Exception ex) {
                    return new JsonResult(new ErrorViewModel {
                        Titulo = "Ler Arquivo dos ritmos de bateria",
                        Mensagem = ex.Message
                    });
                }

            return null;
        }

        public static string GetAcordes(string cifraTexto, int tom = 0)
        {
            if (cifraTexto.Contains("<b") == false)
                return SearchAcordes(cifraTexto);

            var notas = cifraTexto.Split("<b");                                             
            var texto = new List<string>();

            for (int i = 0; i < notas.Length; i++)
            {
                if (i == 0)
                    texto.Add(notas[i]);
                else
                {
                    var cifra = notas[i].Split('>')[1].Split('<')[0];
                    var linhaRestante = notas[i].Split('<')[1];

                    var cifraFormatada = cifra;

                    var retorno = GetAcorde(cifraFormatada);
                    var cifraSomenteNota = retorno[0];
                    var cifraAcordeAlteracoes = retorno[1];
                    cifraFormatada = cifraSomenteNota;

                    if (cifraAcordeAlteracoes.Contains('/') == false)
                        cifraFormatada += cifraAcordeAlteracoes;

                    // Vai removendo os últimos caracateres até chegar num que conheça como C#7sus encontra C#7
                    while (Variaveis.notasAcordes.ContainsKey(cifraFormatada) == false)
                        cifraFormatada = cifraFormatada.Remove(cifraFormatada.Length - 1);

                    if (cifraAcordeAlteracoes.Contains('/'))
                        cifraFormatada += cifraAcordeAlteracoes;

                    if (string.IsNullOrEmpty(cifraFormatada) == false)
                    {
                        if (tom != 0)
                        {
                            cifraFormatada = MudarCifraTom(tom, cifraSomenteNota);

                            if (cifraAcordeAlteracoes.Contains('/'))
                                cifraAcordeAlteracoes = "/" + MudarCifraTom(tom, cifraAcordeAlteracoes.Split('/')[1]);

                            cifraFormatada += cifraAcordeAlteracoes;
                        }

                        cifraFormatada = ">" + cifraFormatada + "<" + linhaRestante;
                        texto.Add("<b id=\"cifra" + i + "\"" + cifraFormatada);
                    }
                    else
                        texto.Add("       " + linhaRestante);
                }
            }

            return string.Join("", texto);
        }

        static string MudarCifraTom(int tom, string cifraSomenteNota) {
            var acordeIndex = Array.IndexOf(Variaveis.tonsMaiores, cifraSomenteNota);
            acordeIndex += tom;

            if (acordeIndex > 11)
                acordeIndex -= 12;
            else if (acordeIndex < 0)
                acordeIndex += 12;

            return Variaveis.tonsMaiores.ElementAt(acordeIndex);
        }

        public static string SearchAcordes(string cifraTexto) {
            var linhasTexto = cifraTexto.Split('\n');
            var texto = new List<string>();
            var somenteAcordes = @"^[A-Ga-g0-9m#bsus°º+/()| \.]*$";
            var linhaIniciandoComAcorde = @"\b[A-G()]";
            var acordeId = 1;

            texto.Add("<style>.cifraSelecionada{background-color:#ff0}pre{line-height:1.6;font-size:14px}</style><pre>");

            foreach (var linha in linhasTexto)
            {
                var linhaFormatada = linha.Replace("[Intro]", "").Replace("[Solo]", "").Replace("[Final]", "");

                if (!string.IsNullOrEmpty(linha) && Regex.IsMatch(linhaFormatada, somenteAcordes))
                {
                    var acordes = linha.Split(' ');

                    for (int i = 0; i < acordes.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(acordes[i]) && Regex.IsMatch(acordes[i], somenteAcordes) && Regex.IsMatch(acordes[i], linhaIniciandoComAcorde))
                        {
                            //System.Diagnostics.Debug.WriteLine(acordes[i]);
                            var acorde = acordes[i];
                            var solo = "";

                            if (acorde.Contains('.'))
                            {
                                string[] parts = acorde.Split('.', 2); // Dividir a string em duas partes usando o ponto como delimitador

                                acorde = parts[0]; // Armazenar o texto antes do primeiro ponto
                                solo = parts.Length > 1 ? "." + parts[1] : ""; // Armazenar o texto depois do primeiro ponto
                                //solo = solo[0] == '.' ? solo : "";
                            }

                            string[] retorno;
                            if (acorde[0] == '(') {
                                texto.Add("(");
                                retorno = GetAcorde(acorde.Split('(')[1].Replace("|", ""));
                            }
                            else if (acorde[0] == ')') {
                                texto.Add(")");
                                retorno = GetAcorde(acorde.Split(')')[1].Replace("|", ""));
                            }
                            else if (acorde.EndsWith(')')) {
                                texto.Add(")");
                                retorno = GetAcorde(acorde.Split(')')[0].Replace("|", ""));
                            }
                            else if (acorde.EndsWith('(')) {
                                texto.Add("(");
                                retorno = GetAcorde(acorde.Split('(')[0].Replace("|", ""));
                            }
                            else
                                retorno = GetAcorde(acorde.Replace("|", ""));

                            var cifraSomenteNota = retorno[0];
                            var cifraAcordeAlteracoes = retorno[1];
                            var cifraFormatada = cifraSomenteNota + cifraAcordeAlteracoes;

                            // Vai removendo os últimos caracateres até chegar num que conheça como C#7sus encontra C#7
                            var cifraProcurar = cifraFormatada;
                            if (acorde.Contains('/'))
                                cifraProcurar = cifraProcurar.Split('/')[0];

                            while (!Variaveis.notasAcordes.ContainsKey(cifraProcurar))
                                cifraFormatada = cifraFormatada.Remove(cifraFormatada.Length - 1);

                            if (!string.IsNullOrEmpty(cifraFormatada))
                            {
                                cifraFormatada = ">" + cifraFormatada + solo + "</b>";
                                texto.Add("<b id=\"cifra" + acordeId + "\"" + cifraFormatada);

                                if (i != acordes.Length - 1)
                                    texto.Add(" ");

                                acordeId++;
                            }
                            else
                                texto.Add("");
                        }
                        else
                            texto.Add(acordes[i] + " ");
                    }
                    texto.Add(Environment.NewLine);
                }
                else
                    texto.Add(linha + Environment.NewLine);
            }

            texto.RemoveAt(texto.Count - 1);

			texto.Add("</pre>");

            return string.Join("", texto);
        }

        public static string[] GetAcorde(string possivelAcorde)
        {
            possivelAcorde = possivelAcorde.Replace("(9)", "9").Replace("m(5-)", "°").Replace("m7(5-)", "°7").Replace('º', '°');

            var cifraFormatada = possivelAcorde;
            var cifraAcordeAlteracoes = "";

            // Retira acordes compostos mais complexos como C7(9)
            if (possivelAcorde.Contains('('))
                cifraFormatada = possivelAcorde.Split('(')[0];

            if (possivelAcorde.Contains('/') && possivelAcorde.Contains('(') == false) {
                cifraFormatada = possivelAcorde.Split('/')[0];
                var retorno = GetAcorde(possivelAcorde.Split('/')[1]);
                cifraAcordeAlteracoes = "/" + retorno[0];
            }

            var cifraSomenteNota = cifraFormatada;

            if (cifraSomenteNota.Length > 1) {
                if (cifraSomenteNota[1] == '#')
                    cifraSomenteNota = cifraSomenteNota[0] + "#";
                else if (cifraSomenteNota[1] == 'b')
                    cifraSomenteNota = cifraSomenteNota[0] + "b";
                else
                    cifraSomenteNota = cifraSomenteNota[0].ToString();

                if (string.IsNullOrEmpty(cifraAcordeAlteracoes))
                    cifraAcordeAlteracoes = cifraFormatada.Split(cifraSomenteNota)[1];

                cifraSomenteNota = Variaveis.acidentesCorrespondentes[cifraSomenteNota];
            }

            return new string[] { cifraSomenteNota, cifraAcordeAlteracoes };
        }
    }
}
