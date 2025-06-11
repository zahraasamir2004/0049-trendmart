using FurniStyle.API.Attributes;
using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.DTOs.Furnis;
using FurniStyle.Core.IServices.Furnis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FurniStyle.API.Controllers.Furni
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {

        private readonly IFurniService _furniService;

        public FurnitureController(IFurniService furniService)
        {
            _furniService = furniService;
        }

        [HttpGet("AllFurniture")]
        
        public async Task< IActionResult> GetAllFurniture()
        {
           var furnies = await _furniService.GetAllFurnisAsync();
            return Ok(furnies);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFurnitureById(int? id)
        {
            if (id == null) BadRequest("Invalid Id");
            var furni = await _furniService.GetFurniByIdAsync(id.Value);
            if (furni == null) return NotFound();
            return Ok(furni);
        }
       
        [HttpGet("SortingFurnitureByNameAscending")]
        public async Task<IActionResult> SortingFurnitureByNameAscending(string? sort)
        {
            var result = await _furniService.SortingFurnitureByNameAscendingAsync(sort);
            return Ok(result);

        }
       
        [HttpGet("SortingFurnitureByNameDescending")]
        public async Task<IActionResult> SortingFurnitureByNameDescending(string? sort)
        {
            var result = await _furniService.SortingFurnitureByNameDescendingAsync(sort);
            return Ok(result);

        }
       
        [HttpGet("SortingFurnitureByPriceAscending")]
        public async Task<IActionResult> SortingFurnitureByPriceAscending(string? sort)
        {
            var result = await _furniService.SortingFurnitureByPriceAscendingAsync(sort);
            return Ok(result);

        }
        
        [HttpGet("SortingFurnitureByPriceDescending")]
        public async Task<IActionResult> SortingFurnitureByPriceDescending(string? sort)
        {
            var result = await _furniService.SortingFurnitureByPriceDescendingAsync(sort);
            return Ok(result);

        }
        
        [HttpGet("SortingFurnitureByQuantityAscending")]
        public async Task<IActionResult> SortingFurnitureByQuantityAscending(string? sort)
        {
            var result = await _furniService.SortingFurnitureByQuantityAscendingAsync(sort);
            return Ok(result);

        }
        
        [HttpGet("SortingFurnitureByQuantityDescending")]
        public async Task<IActionResult> SortingFurnitureByQuantityDescending(string? sort)
        {
            var result = await _furniService.SortingFurnitureByQuantityDescendingAsync(sort);
            return Ok(result);

        }
      
        [HttpGet("SearchFurnitureByName")]
        public async Task<IActionResult> SearchFurnitureByNameAsyc(string? search)
        {
            var result = await _furniService.SearchFurnitureByNameAsyc(search);
            return Ok(result);

        }
        
        [HttpGet("GetAllFurnisInCategoryByCategoryName")]
        public async Task<IActionResult> GetAllFurnisInCategoryByCategoryName(string? category)
        {
            var result = await _furniService.GetAllFurnisInCategoryByCategoryNameAsyc(category);
            return Ok(result);

        }
      
        [HttpGet("GetAllFurnisInRoomByRoomName")]
        public async Task<IActionResult> GetAllFurnisInRoomByRoomName(string? room)
        {
            var result = await _furniService.GetAllFurnisInRoomByRoomNameAsyc(room);
            return Ok(result);

        }
         
        [HttpGet("GetAllFurnisBetweenTwoPrices")]
        public async Task<IActionResult> GetAllFurnisBetweenTwoPrices(decimal? price1, decimal? price2)
        {
            var result = await _furniService.GetAllFurnisBetweenTwoPricesAsyc(price1,price2);
            return Ok(result);

        }
      
        [HttpGet("ApplyingPaginationOnFurnis")]
        public async Task<IActionResult> ApplyingPaginationOnFurnis(int? pageIndex, int? pageSize)
        {
            var result = await _furniService.ApplyingPaginationOnFurnisAsyc(pageIndex.Value, pageSize.Value);
            return Ok(result);

        }
       
        [Authorize("Admin")]
        [HttpPost("CreateFurniture")]
        public async Task<IActionResult> CreateFurni(FurniProcessesDTO furniProcessesDTO)
        {
            try
            {
                await _furniService.AddFurniAsync(furniProcessesDTO);
                return Ok("The Furniture Has Been Successfully Created and Added To The Database.");
            }
            catch (Exception)
            {
               return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There Was an Error While Creating The Furniture. Please, Check Your Input Data"));

            }
        }

        [Authorize("Admin")]
        [HttpPut("UpdateFurnitureByName/{name}")]
        public async Task<IActionResult> UpdateFurnitureByName(string name, [FromBody] FurniProcessesDTO furniProcessesDTO)
        {
            try
            {
                await _furniService.UpdateFurni(name, furniProcessesDTO);
                return Ok($"The furniture '{name}' has been successfully updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
        }

        [Authorize("Admin")]
        [HttpDelete("DeleteFurnitureByName/{name}")]
        public async Task<IActionResult> DeleteFurnitureByName(string name)
        {
            try
            {
                await _furniService.DeleteFurni(name);
                return Ok($"The furniture '{name}' has been successfully Deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
        }


    }
}
