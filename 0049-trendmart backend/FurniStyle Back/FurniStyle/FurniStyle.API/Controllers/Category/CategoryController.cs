using FurniStyle.API.ErrorHandling;
using FurniStyle.Core.DTOs.Furnis;
using FurniStyle.Core.IServices.Categories;
using FurniStyle.Core.IServices.Furnis;
using FurniStyle.Service.Services.Furnis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurniStyle.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("AllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategorisAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int? id)
        {
            if (id == null) BadRequest("Invalid Id");
            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet("SortingCategoryByNameAscending")]
        public async Task<IActionResult> SortingCategoryByNameAscending(string? sort)
        {
            var result = await _categoryService.SortingCategoryByNameAscendingAsync(sort);
            return Ok(result);

        }

        [HttpGet("SortingCategoryByNameDescending")]
        public async Task<IActionResult> SortingCategoryByNameDescending(string? sort)
        {
            var result = await _categoryService.SortingCategoryByNameDescendingAsync(sort);
            return Ok(result);

        }

        [HttpGet("SearchCategoryByName")]
        public async Task<IActionResult> SearchCategoryByName(string? search)
        {
            var result = await _categoryService.SearchCategoryByNameAsyc(search);
            return Ok(result);

        }

        [HttpGet("ApplyingPaginationOnCategories")]
        public async Task<IActionResult> ApplyingPaginationOnCategories(int? pageIndex, int? pageSize)
        {
            var result = await _categoryService.ApplyingPaginationOnCategoryAsyc(pageIndex.Value, pageSize.Value);
            return Ok(result);

        }

        [Authorize("Admin")]
        [HttpPost("CreateCategory/{name}")]
        public async Task<IActionResult> CreateCategory(string name)
        {
            try
            {
                await _categoryService.AddCategoryAsync(name);
                return Ok("The Category Has Been Successfully Created and Added To The Database.");
            }
            catch (Exception)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There Was an Error While Creating The Category. Please, Check Your Input Data"));

            }
        }

        [Authorize("Admin")]
        [HttpPut("UpdateCategory/{oldName}/{newName}")]
        public async Task<IActionResult> UpdateCategory(string oldName,string newName)
        {
            try
            {
                 await _categoryService.UpdateCategory(oldName,newName);
                return Ok("The Category Has Been Successfully Updated ");
            }
            catch (Exception)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There Was an Error While Updating The Category. Please, Check Your Input Data"));

            }
        }

        [Authorize("Admin")]
        [HttpDelete("DeleteCategory/{name}")]
        public async Task<IActionResult> DeleteCategory(string name)
        {
            try
            {
                 await _categoryService.DeleteCategory(name);
                return Ok("The Category Has Been Successfully Deleted.");
            }
            catch (Exception)
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "There Was an Error While Deleting The Category. Please, Check Your Input Data"));

            }
        }


    }
}
