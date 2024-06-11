using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentManagement.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase {
        private readonly PaymentConnector _paymentConnector;

        public PaymentController(PaymentConnector paymentConnector) {
            _paymentConnector = paymentConnector;
        }

        [HttpPost("send")]
        public IActionResult SendPayment([FromBody] object payment) {
            _paymentConnector.SendPayment(payment);
            return Ok("Payment sent.");
        }

    }
}
