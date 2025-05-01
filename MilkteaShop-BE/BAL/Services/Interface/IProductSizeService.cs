using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IProductSizeService
    {
        Task<ICollection<ProductSizeResponseDto>> GetAllProductSizesAsync();
        Task<ProductSize> GetProductSizeByIdAsync(Guid id);
        Task CreateProductSizeAsync(ProductSizeRequestDto productSizeDto);
        Task UpdateProductSizeAsync(Guid id, ProductSizeRequestDto productSizeDto);
        Task<bool> DeleteProductSizeAsync(Guid id);
    }
}
