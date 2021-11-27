using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Topshelf;


namespace DLNLTT
{
    class Program
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x => // Setup các thông tin cần thiết
            {
                    
                x.Service<ThuThapSoLieu>(s => // Khai báo các callBack
                {
                    s.ConstructUsing(thuthapsl => new ThuThapSoLieu());
                    s.WhenStarted(thuthapsl => thuthapsl.Start());
                    s.WhenStopped(thuthapsl => thuthapsl.Stop());

                });
                x.StartAutomatically();
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
            Console.ReadLine();
            
        }
    }
}
