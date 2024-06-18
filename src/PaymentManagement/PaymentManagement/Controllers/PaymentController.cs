using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentManagement.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase {
        private readonly PaymentConnector _paymentConnector;
        private string paymentExchangeName = "PaymentSolArchExchange";
        private string paymentRoutingKey = "payment-sol-arch-routing-key";
        private string paymentQueueName = "PaymentQueue";

        public PaymentController(PaymentConnector paymentConnector) {
            _paymentConnector = paymentConnector;
        }

        [HttpPost("send")]
        public IActionResult SendPayment([FromBody] object payment) {
            _paymentConnector.PaymentSender(payment, paymentQueueName );
            return Ok("Payment sent.");
        }


        // Send Student management information about payment authorization
        [HttpPost("PaymentAuthorization")]
        public IActionResult AuthorizePayment([FromBody] object body) {
            _paymentConnector.PaymentSender(body, paymentQueueName);
            return Ok("Payment received.");
        }

        // Send Student management information about payment success
        [HttpPost("Pay")]
        public IActionResult ConfirmPayment([FromBody] object body) {
            _paymentConnector.PaymentSender(body, paymentQueueName);
            return Ok("Payment received.");
        }



    }
}
