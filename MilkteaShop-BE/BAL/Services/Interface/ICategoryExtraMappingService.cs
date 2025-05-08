using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface ICategoryExtraMappingService
    {
        Task<ICollection<CategoryExtraMappingResponseDto>> GetAllCategoryExtraMappingAsync();

        Task<CategoryExtraMappingResponseDto> GetCategoryExtraMappingByIdAsync(Guid id);

        Task CreateCategoryExtraMappingAsync(CategoryExtraMappingDto categoryExtraMappingDto);

        Task UpdateCategoryExtraMappingAsync(Guid id, CategoryExtraMappingDto categoryExtraMappingDto);

        Task<bool> DeleteCategoryExtraMappingAsync(Guid id);
    }
}
