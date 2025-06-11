using FurniStyle.Core.DTOs.Furnis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Core.IServices.Furnis
{
    public interface IFurniService
    {
        Task<IEnumerable<FurniDTO>> GetAllFurnisAsync();
        Task<FurniDTO> GetFurniByIdAsync(int id);
        Task<IEnumerable<FurniDTO>> SortingFurnitureByNameAscendingAsync(string? sort);
        Task<IEnumerable<FurniDTO>> SortingFurnitureByPriceAscendingAsync(string? sort);
        Task<IEnumerable<FurniDTO>> SortingFurnitureByQuantityAscendingAsync(string? sort);
        Task<IEnumerable<FurniDTO>> SortingFurnitureByNameDescendingAsync(string? sort);
        Task<IEnumerable<FurniDTO>> SortingFurnitureByPriceDescendingAsync(string? sort);
        Task<IEnumerable<FurniDTO>> SortingFurnitureByQuantityDescendingAsync(string? sort);
        Task<FurniWithoutIncludesIdDTO> SearchFurnitureByNameAsyc(string? search);
        Task<IEnumerable<FurniWithoutIncludesIdDTO>> GetAllFurnisInRoomByRoomNameAsyc(string? room);
        Task<IEnumerable<FurniWithoutIncludesIdDTO>> GetAllFurnisInCategoryByCategoryNameAsyc(string? category);
        Task<IEnumerable<FurniWithoutIncludesIdDTO>> GetAllFurnisBetweenTwoPricesAsyc(decimal? price1 , decimal? price2);
        Task<IEnumerable<FurniWithoutIncludesIdDTO>> ApplyingPaginationOnFurnisAsyc(int pageIndex, int pageSize);
        Task AddFurniAsync(FurniProcessesDTO furniProcessesDTO);
        Task UpdateFurni(string name, FurniProcessesDTO furniProcessesDTO);
        Task DeleteFurni(string name);
    }
}
