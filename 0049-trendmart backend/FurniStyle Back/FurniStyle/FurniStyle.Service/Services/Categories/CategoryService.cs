using AutoMapper;
using FurniStyle.Core.DTOs.Categories;
using FurniStyle.Core.DTOs.Furnis;
using FurniStyle.Core.Entities;
using FurniStyle.Core.IServices.Categories;
using FurniStyle.Core.IUnitOfWork;
using FurniStyle.Repository.Specifications.Categories;
using FurniStyle.Repository.Specifications.Furnies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Service.Services.Categories
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategorisAsync()
        {
            var specifications = new CategorySpecifications();
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _unitOfWork.Repository<Category, int>().GetAllAsync(specifications));
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var specifications = new CategorySpecifications(id);
            return _mapper.Map<CategoryDTO>(await _unitOfWork.Repository<Category, int>().GetAsync(specifications));

        }

        public async Task AddCategoryAsync(string name)
        {
            var category = new Category { Name = name };
            await _unitOfWork.Repository<Category, int>().AddAsync(category);
            await _unitOfWork.CompleteAsync();  

        }

        public async Task UpdateCategory(string oldName, string newName)
        {
            var specifications = new CategorySpecifications(oldName, "");
            var category = await  _unitOfWork.Repository<Category, int>().GetAsync(specifications);

            if (category == null)
                throw new KeyNotFoundException($"Category with name '{oldName}' not found.");

            category.Name = newName;

            _unitOfWork.Repository<Category, int>().Update(category); 

            await _unitOfWork.CompleteAsync(); 
        }

        public async Task DeleteCategory(string name)
        {
            var specifications = new CategorySpecifications(name, "");
            var existingCategory = await _unitOfWork.Repository<Category, int>().GetAsync(specifications);
            _unitOfWork.Repository<Category, int>().Delete(existingCategory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> SortingCategoryByNameAscendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }

        public async Task<IEnumerable<CategoryDTO>> SortingCategoryByNameDescendingAsync(string? sort)
        {
            var sortingResult = Sorting(sort);
            return await sortingResult;
        }
       
        public async Task<CategoryDTO> SearchCategoryByNameAsyc(string? search)
        {
            var specifications = new CategorySpecifications(search, "");
            return _mapper.Map<CategoryDTO>(await _unitOfWork.Repository<Category, int>().GetAsync(specifications));
        }

        public async Task<IEnumerable<CategoryDTO>> ApplyingPaginationOnCategoryAsyc(int pageIndex, int pageSize)
        {
            var specifications = new CategorySpecifications(pageIndex, pageSize);
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _unitOfWork.Repository<Category, int>().GetAllAsync(specifications));
        }

        private async Task<IEnumerable<CategoryDTO>> Sorting(string? sort)
        {
            var specifications = new CategorySpecifications(sort);
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _unitOfWork.Repository<Category, int>().GetAllAsync(specifications));
        }
    }
}
