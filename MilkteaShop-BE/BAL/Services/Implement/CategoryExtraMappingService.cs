using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Implements;

namespace BAL.Services.Implement
{
    public class CategoryExtraMappingService : ICategoryExtraMappingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryExtraMappingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryExtraMapping>> GetAllCategoryExtraMappingAsync()
        {
            ICollection<CategoryExtraMapping> categoryExtraMappings = await _unitOfWork.CategoryExtraMappings.GetAllAsync();
            if (categoryExtraMappings == null)
            {
                throw new Exception("No categoryExtraMappings found");
            }
            return categoryExtraMappings;
        }

        public async Task<CategoryExtraMapping> GetCategoryExtraMappingByIdAsync(Guid id)
        {
            CategoryExtraMapping? categoryExtraMappings = await _unitOfWork.CategoryExtraMappings.GetAsync(c => c.Id == id);
            if (categoryExtraMappings == null)
            {
                throw new Exception("CategoryExtraMappings not found");
            }
            return categoryExtraMappings;
        }

        public async Task CreateCategoryExtraMappingAsync(CategoryExtraMappingDto categoryExtraMappingDto)
        {
            CategoryExtraMapping categoryExtraMappings = _mapper.Map<CategoryExtraMapping>(categoryExtraMappingDto);
            await _unitOfWork.CategoryExtraMappings.AddAsync(categoryExtraMappings);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryExtraMappingAsync(Guid id, CategoryExtraMappingDto categoryExtraMappingDto)
        {
            CategoryExtraMapping? categoryExtraMappings = await _unitOfWork.CategoryExtraMappings.GetAsync(c => c.Id == id);
            if (categoryExtraMappings == null)
            {
                throw new Exception("Category not found");
            }
            _mapper.Map(categoryExtraMappingDto, categoryExtraMappings);

            categoryExtraMappings.MainCategoryId = categoryExtraMappingDto.MainCategoryId;
            categoryExtraMappings.ExtraCategoryId = categoryExtraMappingDto.ExtraCategoryId;



            await _unitOfWork.CategoryExtraMappings.UpdateAsync(categoryExtraMappings);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteCategoryExtraMappingAsync(Guid id)
        {
            CategoryExtraMapping? categoryExtraMappings = await _unitOfWork.CategoryExtraMappings.GetAsync(c => c.Id == id);
            if (categoryExtraMappings == null)
            {
                return false;
            }
            await _unitOfWork.CategoryExtraMappings.RemoveAsync(categoryExtraMappings);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}

