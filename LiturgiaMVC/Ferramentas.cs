using LiturgiaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var host = httpContext.Request.Host.Value;
            var path = httpContext.Request.Path.Value;

            var dataHoraBrasilia = DateTime.Now.AddHours(3);
            var dataHora = dataHoraBrasilia.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));

            try
            {
                if (File.Exists(Variaveis.arquivoIPs) == false)
                    File.WriteAllText(Variaveis.arquivoIPs, "IP;EndPoint;Data e Hora");

                File.AppendAllText(Variaveis.arquivoIPs, Environment.NewLine + ip + ";" + host + path + ";" + dataHora);
            }
            catch { }
        }

        public static void LerArquivoAcordesLista()
        {
            var tonsMaiores = new List<string>();
            var tonsMenores = new List<string>();

            if (File.Exists(Variaveis.arquivoAcordesLista) == false)
                File.WriteAllText(Variaveis.arquivoAcordesLista, "C, D, E, G, A");

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
            var notasLinksDict = new Dictionary<string, string>();

            if (File.Exists(Variaveis.arquivoNotasLinks) == false) {
                File.WriteAllText(Variaveis.arquivoNotasLinks,
                    "orgao_do, " + Variaveis.pastaSonsOrgao + "orgao do.ogg" + Environment.NewLine +
                    "orgao_do#, " + Variaveis.pastaSonsOrgao + "orgao do_.ogg" + Environment.NewLine +
                    "orgao_re, " + Variaveis.pastaSonsOrgao + "orgao re.ogg" + Environment.NewLine +
                    "orgao_re#, " + Variaveis.pastaSonsOrgao + "orgao re_.ogg" + Environment.NewLine +
                    "orgao_mi, " + Variaveis.pastaSonsOrgao + "orgao mi.ogg" + Environment.NewLine +
                    "orgao_fa, " + Variaveis.pastaSonsOrgao + "orgao fa.ogg" + Environment.NewLine +
                    "orgao_fa#, " + Variaveis.pastaSonsOrgao + "orgao fa_.ogg" + Environment.NewLine +
                    "orgao_sol, " + Variaveis.pastaSonsOrgao + "orgao sol.ogg" + Environment.NewLine +
                    "orgao_sol#, " + Variaveis.pastaSonsOrgao + "orgao sol_.ogg" + Environment.NewLine +
                    "orgao_la, " + Variaveis.pastaSonsOrgao + "orgao la.ogg" + Environment.NewLine +
                    "orgao_la#, " + Variaveis.pastaSonsOrgao + "orgao la_.ogg" + Environment.NewLine +
                    "orgao_si, " + Variaveis.pastaSonsOrgao + "orgao si.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_do_baixo, " + Variaveis.pastaSonsOrgao + "orgao do baixo.ogg" + Environment.NewLine +
                    "orgao_do#_baixo, " + Variaveis.pastaSonsOrgao + "orgao do_ baixo.ogg" + Environment.NewLine +
                    "orgao_re_baixo, " + Variaveis.pastaSonsOrgao + "orgao re baixo.ogg" + Environment.NewLine +
                    "orgao_re#_baixo, " + Variaveis.pastaSonsOrgao + "orgao re_ baixo.ogg" + Environment.NewLine +
                    "orgao_mi_baixo, " + Variaveis.pastaSonsOrgao + "orgao mi baixo.ogg" + Environment.NewLine +
                    "orgao_fa_baixo, " + Variaveis.pastaSonsOrgao + "orgao fa baixo.ogg" + Environment.NewLine +
                    "orgao_fa#_baixo, " + Variaveis.pastaSonsOrgao + "orgao fa_ baixo.ogg" + Environment.NewLine +
                    "orgao_sol_baixo, " + Variaveis.pastaSonsOrgao + "orgao sol baixo.ogg" + Environment.NewLine +
                    "orgao_sol#_baixo, " + Variaveis.pastaSonsOrgao + "orgao sol_ baixo.ogg" + Environment.NewLine +
                    "orgao_la_baixo, " + Variaveis.pastaSonsOrgao + "orgao la baixo.ogg" + Environment.NewLine +
                    "orgao_la#_baixo, " + Variaveis.pastaSonsOrgao + "orgao la_ baixo.ogg" + Environment.NewLine +
                    "orgao_si_baixo, " + Variaveis.pastaSonsOrgao + "orgao si baixo.ogg"
                );
            }

            var linhas = File.ReadAllLines(Variaveis.arquivoNotasLinks);

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
                    "orgao_C_mao, " + Variaveis.pastaSons + "orgao C mao.ogg" + Environment.NewLine +
                    "orgao_Cm_mao, " + Variaveis.pastaSons + "orgao Cm mao.ogg" + Environment.NewLine +
                    "orgao_C#_mao, " + Variaveis.pastaSons + "orgao C_ mao.ogg" + Environment.NewLine +
                    "orgao_C#m_mao, " + Variaveis.pastaSons + "orgao C_m mao.ogg" + Environment.NewLine +
                    "orgao_D_mao, " + Variaveis.pastaSons + "orgao D mao.ogg" + Environment.NewLine +
                    "orgao_Dm_mao, " + Variaveis.pastaSons + "orgao Dm mao.ogg" + Environment.NewLine +
                    "orgao_D#_mao, " + Variaveis.pastaSons + "orgao D_ mao.ogg" + Environment.NewLine +
                    "orgao_D#m_mao, " + Variaveis.pastaSons + "orgao D_m mao.ogg" + Environment.NewLine +
                    "orgao_E_mao, " + Variaveis.pastaSons + "orgao E mao.ogg" + Environment.NewLine +
                    "orgao_Em_mao, " + Variaveis.pastaSons + "orgao Em mao.ogg" + Environment.NewLine +
                    "orgao_Eb_mao, " + Variaveis.pastaSons + "orgao D_ mao.ogg" + Environment.NewLine +
                    "orgao_Ebm_mao, " + Variaveis.pastaSons + "orgao D_m mao.ogg" + Environment.NewLine +
                    "orgao_F_mao, " + Variaveis.pastaSons + "orgao F mao.ogg" + Environment.NewLine +
                    "orgao_Fm_mao, " + Variaveis.pastaSons + "orgao Fm mao.ogg" + Environment.NewLine +
                    "orgao_F#_mao, " + Variaveis.pastaSons + "orgao F_ mao.ogg" + Environment.NewLine +
                    "orgao_F#m_mao, " + Variaveis.pastaSons + "orgao F_m mao.ogg" + Environment.NewLine +
                    "orgao_G_mao, " + Variaveis.pastaSons + "orgao G mao.ogg" + Environment.NewLine +
                    "orgao_Gm_mao, " + Variaveis.pastaSons + "orgao Gm mao.ogg" + Environment.NewLine +
                    "orgao_G#_mao, " + Variaveis.pastaSons + "orgao G_ mao.ogg" + Environment.NewLine +
                    "orgao_G#m_mao, " + Variaveis.pastaSons + "orgao G_m mao.ogg" + Environment.NewLine +
                    "orgao_A_mao, " + Variaveis.pastaSons + "orgao A mao.ogg" + Environment.NewLine +
                    "orgao_Am_mao, " + Variaveis.pastaSons + "orgao Am mao.ogg" + Environment.NewLine +
                    "orgao_A#_mao, " + Variaveis.pastaSons + "orgao A_ mao.ogg" + Environment.NewLine +
                    "orgao_A#m_mao, " + Variaveis.pastaSons + "orgao A_m mao.ogg" + Environment.NewLine +
                    "orgao_B_mao, " + Variaveis.pastaSons + "orgao B mao.ogg" + Environment.NewLine +
                    "orgao_Bm_mao, " + Variaveis.pastaSons + "orgao Bm mao.ogg" + Environment.NewLine +
                    "orgao_Bb_mao, " + Variaveis.pastaSons + "orgao A_ mao.ogg" + Environment.NewLine +
                    "orgao_Bbm_mao, " + Variaveis.pastaSons + "orgao A_m mao.ogg" + Environment.NewLine +
                    Environment.NewLine +
                    "orgao_C_baixo, " + Variaveis.pastaSons + "orgao C baixo.ogg" + Environment.NewLine +
                    "orgao_Cm_baixo, " + Variaveis.pastaSons + "orgao C baixo.ogg" + Environment.NewLine +
                    "orgao_C#_baixo, " + Variaveis.pastaSons + "orgao C_ baixo.ogg" + Environment.NewLine +
                    "orgao_C#m_baixo, " + Variaveis.pastaSons + "orgao C_ baixo.ogg" + Environment.NewLine +
                    "orgao_D_baixo, " + Variaveis.pastaSons + "orgao D baixo.ogg" + Environment.NewLine +
                    "orgao_Dm_baixo, " + Variaveis.pastaSons + "orgao D baixo.ogg" + Environment.NewLine +
                    "orgao_D#_baixo, " + Variaveis.pastaSons + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_D#m_baixo, " + Variaveis.pastaSons + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_E_baixo, " + Variaveis.pastaSons + "orgao E baixo.ogg" + Environment.NewLine +
                    "orgao_Em_baixo, " + Variaveis.pastaSons + "orgao E baixo.ogg" + Environment.NewLine +
                    "orgao_Eb_baixo, " + Variaveis.pastaSons + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_Ebm_baixo, " + Variaveis.pastaSons + "orgao D_ baixo.ogg" + Environment.NewLine +
                    "orgao_F_baixo, " + Variaveis.pastaSons + "orgao F baixo.ogg" + Environment.NewLine +
                    "orgao_Fm_baixo, " + Variaveis.pastaSons + "orgao F baixo.ogg" + Environment.NewLine +
                    "orgao_F#_baixo, " + Variaveis.pastaSons + "orgao F_ baixo.ogg" + Environment.NewLine +
                    "orgao_F#m_baixo, " + Variaveis.pastaSons + "orgao F_ baixo.ogg" + Environment.NewLine +
                    "orgao_G_baixo, " + Variaveis.pastaSons + "orgao G baixo.ogg" + Environment.NewLine +
                    "orgao_Gm_baixo, " + Variaveis.pastaSons + "orgao G baixo.ogg" + Environment.NewLine +
                    "orgao_G#_baixo, " + Variaveis.pastaSons + "orgao G_ baixo.ogg" + Environment.NewLine +
                    "orgao_G#m_baixo, " + Variaveis.pastaSons + "orgao G_ baixo.ogg" + Environment.NewLine +
                    "orgao_A_baixo, " + Variaveis.pastaSons + "orgao A baixo.ogg" + Environment.NewLine +
                    "orgao_Am_baixo, " + Variaveis.pastaSons + "orgao A baixo.ogg" + Environment.NewLine +
                    "orgao_A#_baixo, " + Variaveis.pastaSons + "orgao A_ baixo.ogg" + Environment.NewLine +
                    "orgao_A#m_baixo, " + Variaveis.pastaSons + "orgao A_ baixo.ogg" + Environment.NewLine +
                    "orgao_B_baixo, " + Variaveis.pastaSons + "orgao B baixo.ogg" + Environment.NewLine +
                    "orgao_Bm_baixo, " + Variaveis.pastaSons + "orgao B baixo.ogg" + Environment.NewLine +
                    "orgao_Bb_baixo, " + Variaveis.pastaSons + "orgao A_ baixo.ogg" + Environment.NewLine +
                    "orgao_Bbm_baixo, " + Variaveis.pastaSons + "orgao A_ baixo.ogg"
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
            {
                var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                File.WriteAllText(Variaveis.arquivonotasAcordes, JsonConvert.SerializeObject(Variaveis.notasAcordes, settings));
            }

            var texto = File.ReadAllText(Variaveis.arquivonotasAcordes).Replace(Environment.NewLine, "").Replace(@"\s+", "").Replace("],}", "]}");

            if (string.IsNullOrEmpty(texto))
                throw new ArgumentException("Arquivo de notas vazio: " + Variaveis.arquivonotasAcordes);
            
            Variaveis.textoNotasAcordes = texto;
        }

        public static JsonResult? CarregarVariaveis()
        {
            if (Variaveis.textoNotasAcordes == "")
                try {
                    Ferramentas.LerArquivoNotasAcordes();
                }
                catch (Exception ex) {
                    return new JsonResult("Error", new ErrorViewModel {                    
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
                try {
                    Ferramentas.LerArquivoAcordesLista();
                }
                catch (Exception ex) {
                    return new JsonResult("Error", new ErrorViewModel {                    
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
    }
}
