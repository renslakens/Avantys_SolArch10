using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ExamManagement;

class Program {
    static async Task Main(string[] args) {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => {
                // Register ExamConnector
                services.AddSingleton<ExamConnector>();

                // Register ExamReceiverService as a hosted service
                services.AddHostedService<ExamReceiverService>();

                // Optional: Add logging for debugging purposes
                services.AddLogging(configure => configure.AddConsole());
            });
    }
}
