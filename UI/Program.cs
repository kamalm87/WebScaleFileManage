using Microsoft.Owin.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using log4net;
using log4net.Config;

namespace OwinSelfhostSample
{
	public class Program
	{
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main()
		{
            
            log.Info("Starting application");

            string baseUrl = "http://localhost:8081/";
			// Start OWIN host 
			//var root = @"C:\projects\LuxRecon\Lux.Recon.UI";
			var root = Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)).Parent.FullName;
			var fileSystem = new PhysicalFileSystem(root);

			WebApp.Start<Startup>(new StartOptions(baseUrl)
			{
				ServerFactory = "Microsoft.Owin.Host.HttpListener"
			});

			// Launch default browser
			Process.Start(baseUrl);

				// Create HttpCient and make a request to api/values 
				HttpClient client = new HttpClient();
				var response2 = client.GetAsync(baseUrl + "/main").Result;
				//Console.WriteLine(response2);
				//Console.WriteLine(response2.Content.ReadAsStringAsync().Result);
			

			Console.ReadLine();
			
		}
	}
}