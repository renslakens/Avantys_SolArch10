using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScheduleManagement.Models;
using ScheduleManagement.Services;

namespace ScheduleManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassController : ControllerBase {
    private readonly ScheduleConnector _scheduleConnector;
    private readonly ClassService _classService;

    public ClassController(ScheduleConnector scheduleConnector, ClassService classService) {
        _scheduleConnector = scheduleConnector;
        _classService = classService;
    }
  
    [HttpPost("send")]
    public IActionResult Send([FromBody] object newClass) {
        _scheduleConnector.ScheduleSender("classCreated", newClass);
        return Ok("Class sent.");
    }
    
    [HttpGet]
    public async Task<List<Class>> GetClasses() {
        return await _classService.GetAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Class>> GetClass(string id) {
        var _class = await _classService.GetAsync(id);
        
        if (_class == null) {
            return NotFound();
        }
        
        return _class;
    }
    
    [HttpGet("scheduleCode/{scheduleCode}")]
    public async Task<Class> GetClassByScheduleCode(string scheduleCode) {
        return await _classService.GetClassByScheduleCode(scheduleCode);
    }
}