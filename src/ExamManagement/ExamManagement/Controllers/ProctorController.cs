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
    public class ProctorController : ControllerBase
    {
        private readonly ProctorsService _proctorsService;

        public ProctorController(ProctorsService proctorsService)
        {
            _proctorsService = proctorsService;
        }

        [HttpGet("fetch")]
        public async Task FetchProctorsAsync()
        {
            await _proctorsService.FetchProctorsAsync();
        }
    }
}