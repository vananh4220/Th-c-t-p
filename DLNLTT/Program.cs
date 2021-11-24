using System;
using HtmlAgilityPack;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Topshelf;

namespace DLNLTT
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<ThuThapSoLieu>(s =>
                {
                    s.ConstructUsing(thuthapsl => new ThuThapSoLieu());
                    s.WhenStarted(thuthapsl => thuthapsl.Start());
                    s.WhenStopped(thuthapsl => thuthapsl.Stop());

                });
                x.RunAsLocalSystem();
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
            Console.ReadLine();
        }

    }

}