using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IProductService
    {
        Task<ICollection<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task CreateAsync(ProductDto productDto);
        Task UpdateAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
