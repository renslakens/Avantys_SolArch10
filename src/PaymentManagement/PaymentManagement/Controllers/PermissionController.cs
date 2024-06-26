using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Models;
using PaymentManagement.RabbitConnectors;
using PaymentManagement.Services;

namespace PaymentManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase {
        private readonly PaymentConnector _paymentConnector;
        private string paymentQueueName = "PaymentQueue";
        private readonly PermissionService _permessionsService;

        public PermissionController(PaymentConnector paymentConnector, PermissionService permessionsService) {
            _paymentConnector = paymentConnector;
            _permessionsService = permessionsService;
        }

        [HttpGet]
        public async Task<List<Permission>> GetPermissions() {
            return await _permessionsService.GetAsync();
        }

        [HttpPost("send")]
        public IActionResult SendPayment([FromBody] object payment) {
            
            _paymentConnector.PaymentSender(payment );
            return Ok("Payment sent.");
        }


        // Send Student management information about payment authorization
        [HttpPost("PaymentAuthorization")]
        public IActionResult AuthorizePayment([FromBody] object body) {
            _paymentConnector.PaymentSender(body);
            return Ok("Payment received.");
        }

        // Send Student management information about payment success
        [HttpPost("Pay")]
        public IActionResult ConfirmPayment([FromBody] object body) {
            _paymentConnector.PaymentSender(body);
            return Ok("Payment received.");
        }



    }
}
