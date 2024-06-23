using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.CommandHandlers;
using ExamManagement.Commands;
using ExamManagement.Entities;
using ExamManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly ExamsService _examsService;
        private readonly ExamConnector _examConnector;
        private string examExchange = "ExamSolArchExchange";
        private string examRoutingKey = "exam-sol-arch-routing-key";
        private readonly IScheduleExamCommandHandler _scheduleExamCommandHandler;

        public ExamController(ExamConnector examConnector, ExamsService examsService, IScheduleExamCommandHandler scheduleExamCommandHandler)
        {
            _examsService = examsService;
            _examConnector = examConnector;
            _scheduleExamCommandHandler = scheduleExamCommandHandler ?? throw new ArgumentNullException(nameof(scheduleExamCommandHandler));
        }

        [HttpGet]
        public async Task<List<Exam>> GetExams()
        {
            return await _examsService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(string id)
        {
            var exam = await _examsService.GetAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        [HttpPost]
        public async Task<ActionResult<Exam>> CreateExam([FromBody] Exam newExam)
        {
            await _examsService.CreateAsync(newExam);
            return CreatedAtAction("GetExam", new { id = newExam.Id }, newExam);
        }

        [HttpPost("Schedule")]
        public async Task<IActionResult> ScheduleExamAsync([FromBody] ScheduleExam command)
        {
            Console.WriteLine("HI " + command.MessageId + command.examId + " " + command.studentId + " " + command.scheduledDate + " " + command.module);
            try
            {
                Exam exam = await _scheduleExamCommandHandler.handleCommandAsync(command);

                if (exam == null)
                {
                    return NotFound();
                }

                Console.WriteLine("Exam scheduled" + exam);
                return Ok(exam);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(string id, [FromBody] Exam updatedExam)
        {
            var exam = await _examsService.GetAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            updatedExam.Id = exam.Id;
            await _examsService.UpdateAsync(id, updatedExam);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var exam = await _examsService.GetAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            await _examsService.DeleteAsync(id);
            return NoContent();
        }

        // [HttpPost("Send")]
        // public IActionResult SendExam([FromBody] Object exam)
        // {
        //     _examConnector.Send(exam, examExchange, examRoutingKey, "ExamQueue");
        //     return Ok("Exam sent successfully");
        // }
    }
}