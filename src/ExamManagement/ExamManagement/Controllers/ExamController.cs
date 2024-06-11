using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly ExamConnector _examConnector;
        private string examExchange = "ExamSolArchExchange";
        private string examRoutingKey = "exam-sol-arch-routing-key";

        public ExamController(ExamConnector examConnector)
        {
            _examConnector = examConnector;
        }

        [HttpPost("send")]
        public IActionResult SendExam([FromBody] Object exam)
        {
            _examConnector.SendExam(exam, examExchange, examRoutingKey, "ExamQueue");
            return Ok("Exam sent successfully");
        }
    }
}