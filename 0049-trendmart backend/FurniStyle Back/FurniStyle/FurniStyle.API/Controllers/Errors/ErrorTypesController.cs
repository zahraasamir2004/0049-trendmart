using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.IServices.Furnis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurniStyle.API.Controllers.Errors
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorTypesController : ControllerBase
    {
        private readonly IFurniService _furniService;

        public ErrorTypesController(IFurniService furniService)
        {
            _furniService = furniService;
        }

        [HttpGet("NotFoundError")]
        public async Task<IActionResult> NotFoundResponse()
        {
            var furni = await _furniService.GetFurniByIdAsync(200);
            if (furni == null) return NotFound(new ApiErrorResponse(404));
            return Ok(furni);
        }

        [HttpGet("ServerError")]
        public async Task<IActionResult> ServerErrorResponse()
        {
            var furni = await _furniService.GetFurniByIdAsync(200);
            var furniString = furni.ToString();
            return Ok(furniString);
        }

        [HttpGet("BadRequestError")]
        public async Task<IActionResult> BadRequestResponse()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("InvalidRequest/{id}")]
        public async Task<IActionResult> InvalidRequest(int id)
        {
            return Ok();
        }

        [HttpGet("UnAuthorizede")]
        public async Task<IActionResult> UnAuthorizedResponse()
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
