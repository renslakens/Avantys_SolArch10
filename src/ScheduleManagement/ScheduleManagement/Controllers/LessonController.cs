using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScheduleManagement.Models;
using ScheduleManagement.Services;

namespace ScheduleManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase {
    private readonly ScheduleConnector _scheduleConnector;
    private readonly LessonService _lessonService;

    public LessonController(ScheduleConnector scheduleConnector, LessonService lessonService) {
        _scheduleConnector = scheduleConnector;
        _lessonService = lessonService;
    }
  
    [HttpPost("send")]
    public IActionResult Send([FromBody] object newClass) {
        _scheduleConnector.ScheduleSender("lessonCreated", newClass);
        return Ok("Lesson sent.");
    }
    
    [HttpGet]
    public async Task<List<Lesson>> GetLessons() {
        return await _lessonService.GetAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Lesson>> GetLesson(string id) {
        var lesson = await _lessonService.GetAsync(id);
        
        if (lesson == null) {
            return NotFound();
        }
        
        return lesson;
    }
    
    [HttpGet("scheduleCode/{classId}")]
    public async Task<List<Lesson>> GetLessonsByClass(string classId) {
        return await _lessonService.GetLessonsByClass(classId);
    }
}