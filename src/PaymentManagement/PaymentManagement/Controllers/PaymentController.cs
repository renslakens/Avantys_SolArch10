using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Models;
using PaymentManagement.RabbitConnectors;
using PaymentManagement.Services;

namespace PaymentManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService) {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<List<Payment>> GetPayments() {
            return await _paymentService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment) {
            await _paymentService.CreateAsync(payment);
            return Ok("Payment created.");
        }




    }
}
