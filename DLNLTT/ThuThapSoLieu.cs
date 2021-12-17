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
    [DisallowConcurrentExecution]
    class ThuThapSoLieu : IJob
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ThuThapSoLieu));

        public Task Execute(IJobExecutionContext context)
        {
            ReadJson rj = new ReadJson();
            var items = rj.GetDataFromJson();

            try
            {
                //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage message = client.GetAsync(items.Url).Result;//thực hiện truy vấn get
                    if (message.IsSuccessStatusCode)
                    {
                        string data = message.Content.ReadAsStringAsync().Result;
                        var htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(data);
                        string token = htmlDocument.DocumentNode.SelectSingleNode("//form[@id='login-form']/input").Attributes["value"].Value;
                        client.DefaultRequestHeaders.Add("Referer", items.Url);//thiết lập header
                        List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>()
                        {
                        new KeyValuePair<string, string>("__RequestVerificationToken", token),
                        new KeyValuePair<string, string>("Username", items.Username),
                        new KeyValuePair<string, string>("Password", items.Password),
                        };
                        FormUrlEncodedContent form = new FormUrlEncodedContent(param);
                        HttpResponseMessage message1 = client.PostAsync(items.Url, form).Result;
                        string result = message1.Content.ReadAsStringAsync().Result;
                        htmlDocument.LoadHtml(result);
                        var document = htmlDocument.DocumentNode.Descendants("div")
                            .Where(node => node.GetAttributeValue("class", "").Equals("m-widget1")).ToList();
                        foreach (var doc in document)
                        {
                            var colNodes = doc.Descendants("h3");
                            var colList = colNodes.ToList();

                            using (var cont = new DLNLTTContext())
                            {
                                var soLieu = new SoLieu
                                {
                                    MtHt = FormatStringInput(colList[1].InnerText.ToString()),
                                    MtCsln = FormatStringInput(colList[2].InnerText.ToString()),
                                    MtTk = FormatStringInput(colList[3].InnerText.ToString()),
                                    MtSln = FormatStringInput(colList[4].InnerText.ToString()),

                                    GHt = FormatStringInput(colList[6].InnerText.ToString()),
                                    GCsln = FormatStringInput(colList[7].InnerText.ToString()),
                                    GTk = FormatStringInput(colList[8].InnerText.ToString()),
                                    GSln = FormatStringInput(colList[9].InnerText.ToString()),

                                    SkHt = FormatStringInput(colList[11].InnerText.ToString()),
                                    SkCsln = FormatStringInput(colList[12].InnerText.ToString()),
                                    SkTk = FormatStringInput(colList[13].InnerText.ToString()),
                                    SkSln = FormatStringInput(colList[14].InnerText.ToString()),

                                    THt = FormatStringInput(colList[16].InnerText.ToString()),
                                    TCsln = FormatStringInput(colList[17].InnerText.ToString()),
                                    TTk = FormatStringInput(colList[18].InnerText.ToString()),
                                    TSln = FormatStringInput(colList[19].InnerText.ToString()),

                                    ThoiGian = DateTime.Now.ToString()
                                };
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
        private string FormatStringInput(string input)
        {
            if( input!= null)
            {
                input = Regex.Replace(input.Trim('\r', '\n').Trim(), @"\s", "");
            }
            return input;
        }
    }
}
