using System;
using Topshelf;


namespace DLNLTT
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x => // Setup các thông tin cần thiết
            {
                    
                x.Service<NLTTService>(s => // Khai báo các callBack
                {
                    s.ConstructUsing(nlttService => new NLTTService());
                    s.WhenStarted(nlttService => nlttService.Start());
                    s.WhenStopped(nlttService => nlttService.Stop());

                });
                x.StartAutomatically();
                x.SetServiceName("NLTTService");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
            Console.ReadLine();         
        }
    }
}
