using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.IServices.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurniStyle.API.Controllers.Payments
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize]
        [HttpPost("CreatePaymentIntent/{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            if(basketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var basket = await _paymentService.CreateUpdatePaymentIntentAsync(basketId);
            if(basket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            
            return Ok(basket);
        }
    }
}
