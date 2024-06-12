namespace ScheduleManagement;

public class ScheduleReceiverService : BackgroundService {
    private readonly ScheduleConnector _scheduleConnector;
    
    public ScheduleReceiverService(ScheduleConnector scheduleConnector) {
        _scheduleConnector = scheduleConnector;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        return Task.Run(() => _scheduleConnector.ScheduleReceiver<dynamic>(), stoppingToken);
    }
}