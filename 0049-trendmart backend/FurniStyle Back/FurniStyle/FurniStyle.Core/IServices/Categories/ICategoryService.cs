using FurniStyle.Core.DTOs.Categories;
using FurniStyle.Core.DTOs.Furnis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IServices.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategorisAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryDTO>> SortingCategoryByNameAscendingAsync(string? sort);
        Task<IEnumerable<CategoryDTO>> SortingCategoryByNameDescendingAsync(string? sort);
        Task<CategoryDTO> SearchCategoryByNameAsyc(string? search);
        Task<IEnumerable<CategoryDTO>> ApplyingPaginationOnCategoryAsyc(int pageIndex, int pageSize);

        Task AddCategoryAsync(string name);
        Task UpdateCategory(string name,string name2);
        Task DeleteCategory(string name);
    }
}
