using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Commands;
using ExamManagement.Entities;
using ExamManagement.Models;
using ExamManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly ExamsService _examsService;
        private string examExchange = "ExamSolArchExchange";
        private string examRoutingKey = "exam-sol-arch-routing-key";

        public ExamController(ExamsService examsService)
        {
            _examsService = examsService;
        }

        [HttpGet]
        public async Task<List<Models.Exam>> GetExams()
        {
            return await _examsService.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Exam>> GetExam(string id)
        {
            var exam = await _examsService.GetAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        [HttpPost("Schedule")]
        public async Task<IActionResult> ScheduleExamAsync([FromBody] ScheduleExam command)
        {
            try
            {
                var exam = await _examsService.ScheduleExamAsync(command);
                if (exam == null)
                {
                    return NotFound();
                }
                return Ok(exam);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Conduct")]
        public async Task<IActionResult> ConductExamAsync([FromBody] ConductExam command)
        {
            Console.WriteLine(command);
            Console.WriteLine("YOOO " + command.Id + " ");
            try
            {
                Console.WriteLine("Conducting exam in controller " + command.Id);
                var exam = await _examsService.ConductExamAsync(command);
                if (exam == null)
                {
                    return NotFound();
                }
                return Ok(exam);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Grade")]
        public async Task<IActionResult> GradeExamAsync([FromBody] GradeExam command)
        {
            try
            {
                var exam = await _examsService.GradeExamAsync(command);
                if (exam == null)
                {
                    return NotFound();
                }
                return Ok(exam);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Publish")]
        public async Task<IActionResult> PublishExamResultAsync([FromBody] PublishResult command)
        {
            try
            {
                var exam = await _examsService.PublishExamResultAsync(command);
                if (exam == null)
                {
                    return NotFound();
                }
                return Ok(exam);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateExam(string id, [FromBody] Exam updatedExam)
        // {
        //     var exam = await _examsService.GetAsync(id);
        //     if (exam == null)
        //     {
        //         return NotFound();
        //     }

        //     updatedExam.Id = exam.Id;
        //     await _examsService.UpdateAsync(id, updatedExam);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteExam(string id)
        // {
        //     var exam = await _examsService.GetAsync(id);
        //     if (exam == null)
        //     {
        //         return NotFound();
        //     }

        //     await _examsService.DeleteAsync(id);
        //     return NoContent();
        // }

    }
}