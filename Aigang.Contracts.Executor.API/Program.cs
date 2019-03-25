using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Aigang.Contracts.Executor.API
{
    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        
        public static void Main(string[] args)
        {
            LoadConfiguration();
            Logger.Info("Program started");
            
            BuildWebHost(args).Run();
        }
        
        static void LoadConfiguration()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var config = new FileInfo("log4net.config");
            XmlConfigurator.Configure(logRepository, config);
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(urls: "http://localhost:5001")
                .Build();
    }
}