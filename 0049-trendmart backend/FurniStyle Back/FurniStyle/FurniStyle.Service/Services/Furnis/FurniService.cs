using AutoMapper;
using FurniStyle.Core.DTOs.Furnis;
using FurniStyle.Core.Entities;
using FurniStyle.Core.IServices.Furnis;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Repository.Specifications.Categories;
using FurniStyle.Repository.Specifications.Furnies;
using FurniStyle.Repository.Specifications.Rooms;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Service.Services.Furnis
{
    public class FurniService : IFurniService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FurniService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FurniDTO>> GetAllFurnisAsync()
        {
            var specifications = new FurniSpecifications();
            return _mapper.Map<IEnumerable<FurniDTO>>(await _unitOfWork.Repository<Furni, int>().GetAllAsync(specifications));
        }

        public async Task<FurniDTO> GetFurniByIdAsync(int id)
        {
            var specifications = new FurniSpecifications(id);
            return _mapper.Map<FurniDTO>(await _unitOfWork.Repository<Furni, int>().GetAsync(specifications));

        }

        public async Task AddFurniAsync(FurniProcessesDTO furniProcessesDTO)
        {
           
            var specifications = new CategorySpecifications(furniProcessesDTO.CategoryName, "");
            var category = await _unitOfWork.Repository<Category, int>().GetAsync(specifications);

            if (category == null)
                throw new Exception("Category not found");

            var specifications2 = new RoomSpecifications(furniProcessesDTO.RoomName, "");
            var room = await _unitOfWork.Repository<Room, int>().GetAsync(specifications2);

            if (room == null)
                throw new Exception("Room not found");

            var furni = _mapper.Map<Furni>(furniProcessesDTO);

            furni.CategoryId = category.Id;
            furni.RoomId = room.Id;

            await _unitOfWork.Repository<Furni, int>().AddAsync(furni);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateFurni(string name, FurniProcessesDTO furniProcessesDTO)
        {
            var specifications = new FurniSpecifications(furniProcessesDTO.Name, "");
            var existingFurni= await _unitOfWork.Repository<Furni, int>().GetAsync(specifications);
        
            if (existingFurni == null)
            {
                throw new Exception($"Furniture with name '{name}' not found.");
            }

            existingFurni.Description = furniProcessesDTO.Description;
            existingFurni.Price = furniProcessesDTO.Price;
            existingFurni.StockQuantity = furniProcessesDTO.StockQuantity;
            existingFurni.PictureUrl = furniProcessesDTO.PictureUrl;

            var specifications2 = new CategorySpecifications(furniProcessesDTO.CategoryName, "");
            var category = await _unitOfWork.Repository<Category, int>().GetAsync(specifications2);

            if (category == null)
                throw new Exception("Category not found");

            var specifications3 = new RoomSpecifications(furniProcessesDTO.RoomName, "");
            var room = await _unitOfWork.Repository<Room, int>().GetAsync(specifications3);

            if (room == null)
                throw new Exception("Room not found");

            existingFurni.CategoryId = category.Id;
            existingFurni.RoomId = room.Id;

            _unitOfWork.Repository<Furni, int>().Update(existingFurni);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteFurni(string name)
        {
            var specifications = new FurniSpecifications(name, "");
            var existingFurni = await _unitOfWork.Repository<Furni, int>().GetAsync(specifications);
            _unitOfWork.Repository<Furni, int>().Delete(existingFurni);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<FurniDTO>> SortingFurnitureByNameAscendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;

        }

        public async Task<IEnumerable<FurniDTO>> SortingFurnitureByPriceAscendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;

        }

        public async Task<IEnumerable<FurniDTO>> SortingFurnitureByQuantityAscendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }

        public async Task<IEnumerable<FurniDTO>> SortingFurnitureByNameDescendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }

        public async Task<IEnumerable<FurniDTO>> SortingFurnitureByPriceDescendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }

        public async Task<IEnumerable<FurniDTO>> SortingFurnitureByQuantityDescendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }

        public async Task<FurniWithoutIncludesIdDTO> SearchFurnitureByNameAsyc(string? search)
        {
            var specifications = new FurniSpecifications(search, "");
            return _mapper.Map<FurniWithoutIncludesIdDTO>(await _unitOfWork.Repository<Furni, int>().GetAsync(specifications));
        }

        public async Task<IEnumerable<FurniWithoutIncludesIdDTO>> GetAllFurnisInRoomByRoomNameAsyc(string? room)
        {
            var specifications = new FurniSpecifications("", room, "");
            return _mapper.Map<IEnumerable<FurniWithoutIncludesIdDTO>>(await _unitOfWork.Repository<Furni, int>().GetAllAsync(specifications));
        }

        public async Task<IEnumerable<FurniWithoutIncludesIdDTO>> GetAllFurnisInCategoryByCategoryNameAsyc(string? category)
        {
            var specifications = new FurniSpecifications(category, "", "");
            return _mapper.Map<IEnumerable<FurniWithoutIncludesIdDTO>>(await _unitOfWork.Repository<Furni, int>().GetAllAsync(specifications));
        }

        public async Task<IEnumerable<FurniWithoutIncludesIdDTO>> GetAllFurnisBetweenTwoPricesAsyc(decimal? price1, decimal? price2)
        {
            var specifications = new FurniSpecifications(price1, price2);
            return _mapper.Map<IEnumerable<FurniWithoutIncludesIdDTO>>(await _unitOfWork.Repository<Furni, int>().GetAllAsync(specifications));
        }

        public async Task<IEnumerable<FurniWithoutIncludesIdDTO>> ApplyingPaginationOnFurnisAsyc(int pageIndex, int pageSize)
        {
            var specifications = new FurniSpecifications(pageIndex, pageSize);
            return _mapper.Map<IEnumerable<FurniWithoutIncludesIdDTO>>(await _unitOfWork.Repository<Furni, int>().GetAllAsync(specifications));
        }

        private async Task<IEnumerable<FurniDTO>> Sorting(string? sort)
        {
            var specifications = new FurniSpecifications(sort);
            return _mapper.Map<IEnumerable<FurniDTO>>(await _unitOfWork.Repository<Furni, int>().GetAllAsync(specifications));
        }


      
    }
}
