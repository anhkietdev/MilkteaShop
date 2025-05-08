using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ICollection<CategoryExtraMappingResponseDto>> GetAllCategoryExtraMappingAsync()
        {
            var categoryExtraMappings = await _unitOfWork.CategoryExtraMappings.GetAllAsync();
            if (categoryExtraMappings == null)
            {
                throw new Exception("No category extra mappings found");
            }

         
            var categoryExtraMappingDtos = categoryExtraMappings.Select(cem =>
            {
                var mainCategory = cem.MainCategoryId != null ? _unitOfWork.Categories.GetAsync(c => c.Id == cem.MainCategoryId).Result : null;
                var extraCategory = cem.ExtraCategoryId != null ? _unitOfWork.Categories.GetAsync(c => c.Id == cem.ExtraCategoryId).Result : null;

                return new CategoryExtraMappingResponseDto
                {
                    Id = cem.Id,
                    MainCategoryId = cem.MainCategoryId,
                    ExtraCategoryId = cem.ExtraCategoryId,
                    MainCategoryName = mainCategory?.CategoryName,
                    ExtraCategoryName = extraCategory?.CategoryName,
                    MainCategoryDescription = mainCategory?.Description,
                    ExtraCategoryDescription = extraCategory?.Description
                };
            }).ToList();

            return categoryExtraMappingDtos;
        }

      
        public async Task<CategoryExtraMappingResponseDto> GetCategoryExtraMappingByIdAsync(Guid id)
        {
            var categoryExtraMapping = await _unitOfWork.CategoryExtraMappings.GetAsync(c => c.Id == id);
            if (categoryExtraMapping == null)
            {
                throw new Exception("CategoryExtraMapping not found");
            }

            var mainCategory = categoryExtraMapping.MainCategoryId != null ? await _unitOfWork.Categories.GetAsync(c => c.Id == categoryExtraMapping.MainCategoryId) : null;
            var extraCategory = categoryExtraMapping.ExtraCategoryId != null ? await _unitOfWork.Categories.GetAsync(c => c.Id == categoryExtraMapping.ExtraCategoryId) : null;

            var categoryExtraMappingDto = new CategoryExtraMappingResponseDto
            {
                Id = categoryExtraMapping.Id,
                MainCategoryId = categoryExtraMapping.MainCategoryId,
                ExtraCategoryId = categoryExtraMapping.ExtraCategoryId,
                MainCategoryName = mainCategory?.CategoryName,
                ExtraCategoryName = extraCategory?.CategoryName,
                MainCategoryDescription = mainCategory?.Description,
                ExtraCategoryDescription = extraCategory?.Description
            };

            return categoryExtraMappingDto;
        }

        
        public async Task CreateCategoryExtraMappingAsync(CategoryExtraMappingDto categoryExtraMappingDto)
        {
            var categoryExtraMapping = _mapper.Map<CategoryExtraMapping>(categoryExtraMappingDto);
            await _unitOfWork.CategoryExtraMappings.AddAsync(categoryExtraMapping);
            await _unitOfWork.SaveAsync();
        }

  
        public async Task UpdateCategoryExtraMappingAsync(Guid id, CategoryExtraMappingDto categoryExtraMappingDto)
        {
            var categoryExtraMapping = await _unitOfWork.CategoryExtraMappings.GetAsync(c => c.Id == id);
            if (categoryExtraMapping == null)
            {
                throw new Exception("CategoryExtraMapping not found");
            }

            _mapper.Map(categoryExtraMappingDto, categoryExtraMapping);
            await _unitOfWork.CategoryExtraMappings.UpdateAsync(categoryExtraMapping);
            await _unitOfWork.SaveAsync();
        }

       
        public async Task<bool> DeleteCategoryExtraMappingAsync(Guid id)
        {
            var categoryExtraMapping = await _unitOfWork.CategoryExtraMappings.GetAsync(c => c.Id == id);
            if (categoryExtraMapping == null)
            {
                return false;
            }

            await _unitOfWork.CategoryExtraMappings.RemoveAsync(categoryExtraMapping);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
