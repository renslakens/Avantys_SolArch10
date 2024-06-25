using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ExamManagement;
using ExamManagement.Models;
using ExamManagement.Repositories;

class Program {
    static async Task Main(string[] args) {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) {
        return Host.CreateDefaultBuilder(args)
            // .ConfigureAppConfiguration((hostingContext, config) => {
            //     config.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
            //     config.AddEnvironmentVariables();
            // })
            .ConfigureServices((hostContext, services) => {
                // Register ExamConnector
                services.AddSingleton<ExamConnector>();
                services.AddSingleton<EventStoreRepository>();
                services.AddSingleton<ReadStoreRepository>();

                // Register ExamReceiverService as a hosted service
                services.AddHostedService<ExamReceiverService>();

                // Optional: Add logging for debugging purposes
                services.AddLogging(configure => configure.AddConsole());
                // services.AddTransient<ExamManagementDatabaseSettings>((svc) => {
                //     var settings = new ExamManagementDatabaseSettings();
                //     hostContext.Configuration.GetSection("ExamEventDatabase").Bind(settings);
                //     return settings;
                // });
                // services.Configure<ExamManagementDatabaseSettings>(
                //     hostContext.Configuration.GetSection("ExamManagementDatabase"));
            });
    }
}
