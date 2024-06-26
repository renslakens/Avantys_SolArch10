using ExamManagement.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScheduleManagement;

class Program {
    static async Task Main(string[] args) {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => {
                services.AddSingleton<ScheduleConnector>();
                services.AddSingleton<ReadStoreRepository>();

                services.AddHostedService<ScheduleReceiverService>();

                services.AddLogging(configure => configure.AddConsole());
            });
    }
}