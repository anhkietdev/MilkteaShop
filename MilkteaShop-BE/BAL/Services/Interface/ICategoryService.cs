using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task CreateCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(Guid id, CategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
