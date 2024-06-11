﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentManagement.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase {
        private readonly PaymentConnector _paymentConnector;
        private string paymentExchangeName = "PaymentSolArchExchange";
        private string paymentRoutingKey = "payment-sol-arch-routing-key";

        public PaymentController(PaymentConnector paymentConnector) {
            _paymentConnector = paymentConnector;
        }

        [HttpPost("send")]
        public IActionResult SendPayment([FromBody] object payment) {
            _paymentConnector.SendPayment(payment, paymentExchangeName, paymentRoutingKey );
            return Ok("Payment sent.");
        }

    }
}