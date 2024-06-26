using Microsoft.Extensions.Hosting;

namespace ScheduleManagement;


public class ScheduleReceiverService : IHostedService {
    private readonly ScheduleConnector _scheduleConnector;
    
    public ScheduleReceiverService(ScheduleConnector scheduleConnector) {
        _scheduleConnector = scheduleConnector;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() => _scheduleConnector.Receive<dynamic>(), cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}