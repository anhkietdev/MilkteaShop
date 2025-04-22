using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            ICollection<Category> categories = await _unitOfWork.Categories.GetAllAsync();
            if (categories == null)
            {
                throw new Exception("No categories found");
            }
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            Category? category = await _unitOfWork.Categories.GetAsync(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Guid id, CategoryDto categoryDto)
        {
            Category? category = await _unitOfWork.Categories.GetAsync(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            _mapper.Map(categoryDto, category);

            category.CategoryName = categoryDto.CategoryName;
            category.Description = categoryDto.Description;
            
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            Category? category = await _unitOfWork.Categories.GetAsync(c => c.Id == id);
            if (category == null)
            {
                return false;
            }
            await _unitOfWork.Categories.RemoveAsync(category);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
