using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductSizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateProductSizeAsync(ProductSizeRequestDto productSizeDto)
        {
            ProductSize productSize = _mapper.Map<ProductSize>(productSizeDto);
            await _unitOfWork.ProductSizes.AddAsync(productSize);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteProductSizeAsync(Guid id)
        {
            ProductSize? productSize = await _unitOfWork.ProductSizes.GetAsync(p => p.Id == id);
            if (productSize == null)
            {
                return false;
            }
            await _unitOfWork.ProductSizes.RemoveAsync(productSize);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ICollection<ProductSize>> GetAllProductSizesAsync()
        {
            ICollection<ProductSize> productSizes = await _unitOfWork.ProductSizes.GetAllAsync();
            if (productSizes == null)
            {
                throw new Exception("No product sizes found");
            }
            return productSizes;
        }

        public async Task<ProductSize> GetProductSizeByIdAsync(Guid id)
        {
            ProductSize? productSize = await _unitOfWork.ProductSizes.GetAsync(p => p.Id == id);
            if (productSize == null)
            {
                throw new Exception("Product size not found");
            }
            return productSize;
        }

        public async Task UpdateProductSizeAsync(Guid id, ProductSizeRequestDto productSizeDto)
        {
            ProductSize? productSize = await _unitOfWork.ProductSizes.GetAsync(p => p.Id == id);
            if (productSize == null)
            {
                throw new Exception("Product size not found");
            }
            _mapper.Map(productSizeDto, productSize);

            productSize.Size = productSizeDto.Size;
            productSize.Price = productSizeDto.Price;
            productSize.ProductId = productSizeDto.ProductId;

            await _unitOfWork.ProductSizes.UpdateAsync(productSize);
            await _unitOfWork.SaveAsync();
        }
    }
}
