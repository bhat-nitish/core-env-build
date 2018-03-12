using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EnvBuild
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args != null)
            {
                for (int i = 0; i < args.Count(); i++)
                {
                    Console.WriteLine($"Arguement-{i + 1}: " + args[i]);
                }
                Startup.args = args; // assign the build arguements to the args of the startup class , the config gets updated as per arguements supplied
            }

            // configure to read from command line, read environment without the ASPNETCORE_ prefix since it is generic
            var configuration = new ConfigurationBuilder()
               .AddCommandLine(args)
               .Build();

            // Use the configuration from command line to run the dotnet application, default to Development if not specified
            var host = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
