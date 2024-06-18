using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ExamConnector _examConnector;
        private string examExchange = "ExamSolArchExchange";
        private string examRoutingKey = "exam-sol-arch-routing-key";

        public ExamController(ExamConnector examConnector, ExamsService examsService)
        {
            _examsService = examsService;
            _examConnector = examConnector;
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

        [HttpPost("Send")]
        public IActionResult SendExam([FromBody] Object exam)
        {
            _examConnector.SendExam(exam, examExchange, examRoutingKey, "ExamQueue");
            return Ok("Exam sent successfully");
        }
    }
}