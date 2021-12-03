using HtmlAgilityPack;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DLNLTT
{
    class ThuThapSoLieu : IJob
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ThuThapSoLieu));

        public Task Execute(IJobExecutionContext context)
        {
            ReadJson rj = new ReadJson();
            var items = rj.GetDataFromJson();

            try
            {
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
                string token = "";
                string Username = items.Username;
                string Password = items.Password;
                string url = items.Url;
                using (HttpClient client = new HttpClient())
                {
                    
                    HttpResponseMessage message = client.GetAsync(url).Result;
                    if (message.IsSuccessStatusCode)
                    {
                        string data = message.Content.ReadAsStringAsync().Result;
                        var htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(data);
                        token = htmlDocument.DocumentNode.SelectSingleNode("//form[@id='login-form']/input").Attributes["value"].Value;
                        client.DefaultRequestHeaders.Add("Referer", url);
                        List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>()

                        {
                        new KeyValuePair<string, string>("__RequestVerificationToken", token),
                        new KeyValuePair<string, string>("Username", Username),
                        new KeyValuePair<string, string>("Password", Password),
                        };
                        FormUrlEncodedContent form = new FormUrlEncodedContent(param);
                        HttpResponseMessage message1 = client.PostAsync(url, form).Result;
                        string result = message1.Content.ReadAsStringAsync().Result;
                        htmlDocument.LoadHtml(result);
                        var document = htmlDocument.DocumentNode.Descendants("div")
                            .Where(node => node.GetAttributeValue("class", "").Equals("m-widget1")).ToList();

                        DateTime now = DateTime.Now;

                        foreach (var doc in document)
                        {
                            var colNodes = doc.Descendants("h3");
                            var colList = colNodes.ToList();

                            string mt_ht = colList[1].InnerText.ToString().Trim('\r', '\n').Trim();
                            string mt_csln = colList[2].InnerText.ToString().Trim('\r', '\n').Trim();
                            string mt_tk = colList[3].InnerText.ToString().Trim('\r', '\n').Trim();
                            string mt_sln = colList[4].InnerText.ToString().Trim('\r', '\n').Trim();

                            string g_ht = colList[6].InnerText.ToString().Trim('\r', '\n').Trim();
                            string g_csln = colList[7].InnerText.ToString().Trim('\r', '\n').Trim();
                            string g_tk = colList[8].InnerText.ToString().Trim('\r', '\n').Trim();
                            string g_sln = colList[9].InnerText.ToString().Trim('\r', '\n').Trim();

                            string sk_ht = colList[11].InnerText.ToString().Trim('\r', '\n').Trim();
                            string sk_csln = colList[12].InnerText.ToString().Trim('\r', '\n').Trim();
                            string sk_tk = colList[13].InnerText.ToString().Trim('\r', '\n').Trim();
                            string sk_sln = colList[14].InnerText.ToString().Trim('\r', '\n').Trim();

                            string t_ht = colList[16].InnerText.ToString().Trim('\r', '\n').Trim();
                            string t_csln = colList[17].InnerText.ToString().Trim('\r', '\n').Trim();
                            string t_tk = colList[18].InnerText.ToString().Trim('\r', '\n').Trim();
                            string t_sln = colList[19].InnerText.ToString().Trim('\r', '\n').Trim();

                            using (var cont = new DLNLTTContext())
                            {
                                var soLieu = new SoLieu
                                {
                                    MtHt = Regex.Replace(mt_ht, @"\s", ""),
                                    MtCsln = Regex.Replace(mt_csln, @"\s", ""),
                                    MtTk = Regex.Replace(mt_tk, @"\s", ""),
                                    MtSln = Regex.Replace(mt_sln, @"\s", ""),

                                    GHt = Regex.Replace(g_ht, @"\s", ""),
                                    GCsln = Regex.Replace(g_csln, @"\s", ""),
                                    GTk = Regex.Replace(g_tk, @"\s", ""),
                                    GSln = Regex.Replace(g_sln, @"\s", ""),

                                    SkHt = Regex.Replace(sk_ht, @"\s", ""),
                                    SkCsln = Regex.Replace(sk_csln, @"\s", ""),
                                    SkTk = Regex.Replace(sk_tk, @"\s", ""),
                                    SkSln = Regex.Replace(sk_sln, @"\s", ""),

                                    THt = Regex.Replace(t_ht, @"\s", ""),
                                    TCsln = Regex.Replace(t_csln, @"\s", ""),
                                    TTk = Regex.Replace(t_tk, @"\s", ""),
                                    TSln = Regex.Replace(t_sln, @"\s", ""),

                                    ThoiGian = now.ToString()
                                };

                                //context.SoLieus.Add(soLieu);

                                cont.Add<SoLieu>(soLieu);

                                cont.SaveChanges();
                                log.InfoFormat(soLieu.ToString(), Encoding.UTF8);

                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                log.Error(ex.Message);

            }
            return Task.CompletedTask;
        }

    }
}
