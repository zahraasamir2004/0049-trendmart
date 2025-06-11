using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.DTOs.Basket;
using FurniStyle.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurniStyle.API.Controllers.Basket
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasket(string id)
        {
            var basket = await _basketService.GetBasketByIdAsync(id);
            if (basket == null)
                return NotFound(new ApiErrorResponse(404));

            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrCreateBasket([FromBody] CustometBasketDTO basketDto)
        {
            if (basketDto == null)
                return BadRequest(new ApiErrorResponse(400));

            var basket = await _basketService.UpdateOrCreateBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _basketService.DeletingBasketAsync(id);
            return NoContent();
        }
    }
}
