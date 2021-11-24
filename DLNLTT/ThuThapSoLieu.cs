using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace DLNLTT
{
    public class ThuThapSoLieu
    {
        private readonly Timer _timer;

        public ThuThapSoLieu()
        {
            _timer = new Timer(10000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var uri = new Uri("http://192.168.68.72:8888/");
                    httpClient.BaseAddress = uri;
                    httpClient.DefaultRequestHeaders.Add("Cookie", "__RequestVerificationToken=FyufirTy8Jdyig4Grb3NrnMSU4gE-9qRJzHe7-UR3ppdY9h1HkJFhjHTannzDz7MKP-0KYNStCtCDiI6QoO1XZ8HUs2KqP7IgGicNE0p3rc1; Cookie1=DA7DFEAC633E5048C74D6D30E56E5DC9FB2BDF84D98C1D6E1272A4B649E8ECB01878A91E9D55EFBA1C09C4B2B04CC1682305415EC1466DD1600F5FB87DA5CFA42C4A5F431865F8E82F62FD47797C87FBEF0EE032CBDCFEB2065B7E6D0B38BFF8F2FD62B2906DE89D87329964AC7919DA189E04B1A47000BE3FFCF31F90DF7403B19DBAFE718EC8998B0FCF65FCA80E5914C65BC83DAA3DFC85C37985F657E2DBF1B3250649935125F86D3C05837DC7AB0D5EB746F472A2349D4809EFB7B21FD336FE8D7A9BE3D4BCD3CA4A3532E6DAF8");
                    var response = httpClient.GetStringAsync(uri).Result;
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(response);

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

                        using (var context = new DLNLTTContext())
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

                            context.Add<SoLieu>(soLieu);

                            context.SaveChanges();
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
