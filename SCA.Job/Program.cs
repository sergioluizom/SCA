using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SCA.Job.Configuration;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Job
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var isDebugging = !(Debugger.IsAttached || args.Contains("--console"));
            var hostBuilder = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<JobService>();
                });
            if (isDebugging)
            {
                await hostBuilder.RunTheServiceAsync();
            }
            else
            {
                await hostBuilder.RunConsoleAsync();
            }
        }
    }
}