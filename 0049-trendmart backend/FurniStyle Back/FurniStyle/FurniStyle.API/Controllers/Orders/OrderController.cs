using AutoMapper;
using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.DTOs.Orders;
using FurniStyle.Core.Entities.OrderEntities;
using FurniStyle.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FurniStyle.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        
        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var address = _mapper.Map<Address>(orderDTO.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(userEmail, orderDTO.BasketId, orderDTO.DelivaryMethodId, address);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<OrderToReturnDTO>(order));
        }
        
        [Authorize]
        [HttpGet("GetOrderForSpecificUser")]
        public async Task<IActionResult> GetOrderForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order = await _orderService.GetOrdersForSpecificUserAsync(userEmail);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            else return Ok(_mapper.Map<IEnumerable<OrderToReturnDTO>>(order));
        }
        
        [Authorize]
        [HttpGet("GetOrderById/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order = await _orderService.GetOrderByIdForSpecificUserAsync(userEmail, orderId);
            if (order is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            else return Ok(_mapper.Map<OrderToReturnDTO>(order));
        }
       
        [HttpGet("DelivaryMethods")]
        public async Task<IActionResult> GetDelivaryMethods()
        {
            var delivaryMethods = await _orderService.GetAllDelivaryMethodsAsync();
            if (delivaryMethods is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(delivaryMethods);
        }

    }
}
