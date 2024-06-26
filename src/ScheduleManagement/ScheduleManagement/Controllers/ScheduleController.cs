using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScheduleManagement.Models;
using ScheduleManagement.Services;

namespace ScheduleManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase {
    private readonly ScheduleConnector _scheduleConnector;
    private readonly ScheduleService _scheduleService;
    private string scheduleExchangeName = "ScheduleSolArchExchange";
    private string scheduleRoutingKey = "schedule-sol-arch-routing-key";
    private string scheduleQueueName = "ScheduleQueue";

    public ScheduleController(ScheduleConnector scheduleConnector, ScheduleService scheduleService) {
        _scheduleConnector = scheduleConnector;
        _scheduleService = scheduleService;
    }
    
    [HttpGet]
    public async Task<List<Schedule>> GetSchedules() {
        return await _scheduleService.GetAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Schedule>> GetSchedule(string id) {
        var schedule = await _scheduleService.GetAsync(id);
        
        if (schedule == null) {
            return NotFound();
        }
        
        return schedule;
    }
    
    [HttpPost]
    public async Task<ActionResult<Schedule>> CreateSchedule([FromBody] Schedule newSchedule) {
        await _scheduleService.CreateAsync(newSchedule);
        return CreatedAtAction("GetSchedule", new { id = newSchedule.Id }, newSchedule);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSchedule(string id, [FromBody] Schedule updatedSchedule) {
        var schedule = await _scheduleService.GetAsync(id);
        if (schedule == null) {
            return NotFound();
        }
        
        updatedSchedule.Id = schedule.Id;
        await _scheduleService.UpdateAsync(id, updatedSchedule);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(string id) {
        var schedule = await _scheduleService.GetAsync(id);
        if (schedule == null) {
            return NotFound();
        }
        
        await _scheduleService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("send")]
    public IActionResult SendSchedule([FromBody] object schedule) {
        _scheduleConnector.ScheduleSender(schedule, scheduleExchangeName, scheduleRoutingKey, scheduleQueueName);
        return Ok("Schedule sent.");
    }
}