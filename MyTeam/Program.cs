using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MyTeam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Check for the Debugger is attached or not if attached then run the application in IIS or IISExpress
            var isService = false;

            //when the service start we need to pass the --service parameter while running the .exe
            if (Debugger.IsAttached == false && args.Contains("--service"))
            {
                isService = true;
            }

            if (isService)
            {
                //Get the Content Root Directory
                var pathToContentRoot = Directory.GetCurrentDirectory();

                //If the args has the console element then than make it in array.
                var webHostArgs = args.Where(arg => arg != "--console").ToArray();

                string ConfigurationFile = ""; //Configuration file.
                string PortNo = "44384"; //Port

                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);

                //Get the json file and read the service port no if available in the json file.
                string AppJsonFilePath = Path.Combine(pathToContentRoot, ConfigurationFile);

                if (File.Exists(AppJsonFilePath))
                {
                    using (StreamReader sr = new StreamReader(AppJsonFilePath))
                    {
                        string jsonData = sr.ReadToEnd();
                        JObject jObject = JObject.Parse(jsonData);
                        if (jObject["ServicePort"] != null)
                            PortNo = jObject["ServicePort"].ToString();
                    }
                }

                var host = WebHost.CreateDefaultBuilder(webHostArgs)
                .UseContentRoot(pathToContentRoot)
                .UseStartup<Startup>()
                .UseUrls("http://51.75.202.18:" + PortNo)
                .Build();

                host.RunAsService();
            }
            else
            {
                CreateHostBuilder(args).Build().Run();
                Console.WriteLine("Promes App started...");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}