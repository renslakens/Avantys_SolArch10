using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentsService _StudentsService;

        public StudentController(StudentsService StudentsService)
        {
            _StudentsService = StudentsService;
        }

        [HttpGet("fetch")]
        public async Task FetchStudentsAsync()
        {
            await _StudentsService.FetchStudentsAsync();
        }
    }
}