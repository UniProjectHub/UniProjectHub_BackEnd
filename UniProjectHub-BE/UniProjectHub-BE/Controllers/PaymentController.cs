using Application.InterfaceServies;
using Domain.Data;
using Domain.Enums;
using Domain.Models;
using Infracstructures.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text;
using UniProjectHub_BE.Services;

namespace UniProjectHub_BE.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ManagePayment _paymentManage;
        private readonly IPaymentService _paymentService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IOptions<PayOSSettings> settings, IPaymentService paymentService, ICurrentUserService currentUserService, ILogger<PaymentController> logger)
        {
            _paymentManage = new ManagePayment(settings);
            _paymentService = paymentService;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        //create link and create payment
        [HttpGet]
        public async Task<IActionResult> GetPaymentInfo()
        {
            Users currentUser = await _currentUserService.GetUser();
            Buyer buyer = new Buyer {
                UserId = currentUser.Id,
                Email = currentUser.Email,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName
            };
            try
            {
                var createPaymentResult = await _paymentManage.CreatePaymentUrlRegisterCreator(buyer);
                Payment payment = new Payment {
                    OrderId = createPaymentResult.orderCode + "",
                    UserId = buyer.UserId,
                    Amount = createPaymentResult.amount,
                    PaymentMethod = "PayOS"
                };

                await _paymentService.ProcessPaymentAsync(payment);

                return Ok(createPaymentResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("payment-success/{code}")]
        public async Task<IActionResult> PaymentSuccess(string code)
        {
            var decode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            try
            {
                await _paymentService.UpdatePaymentStatusAsync(decode, "Success");
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the payment status.");

                // You can also include the error message in the response, but be cautious about exposing too much information in production
                return StatusCode(500, new { message = "An error occurred while updating the payment status." });
            }
            
            return Ok();
        }

        [HttpGet("payment-fail/{code}")]
        public async Task<IActionResult> PaymentFail(string code)
        {
            var decode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            try
            {
                await _paymentService.UpdatePaymentStatusAsync(decode, "Canceled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the payment status.");

                // You can also include the error message in the response, but be cautious about exposing too much information in production
                return StatusCode(500, new { message = "An error occurred while updating the payment status." });
            }

            return Ok();
        }
    }
}
