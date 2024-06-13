using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase {
    private readonly ScheduleConnector _scheduleConnector;
    private string scheduleExchangeName = "ScheduleSolArchExchange";
    private string scheduleRoutingKey = "schedule-sol-arch-routing-key";
    private string scheduleQueueName = "ScheduleQueue";

    public ScheduleController(ScheduleConnector scheduleConnector) {
        _scheduleConnector = scheduleConnector;
    }

    [HttpPost("send")]
    public IActionResult SendSchedule([FromBody] object schedule) {
        _scheduleConnector.ScheduleSender(schedule, scheduleExchangeName, scheduleRoutingKey, scheduleQueueName);
        return Ok("Schedule sent.");
    }
}