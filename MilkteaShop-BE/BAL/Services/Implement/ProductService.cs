using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {            
            Product? product = await _unitOfWork.Products.GetAsync(p => p.Id == id);
            if (product == null)
            {
                return false;
            }
            await _unitOfWork.Products.RemoveAsync(product);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            ICollection<Product> products = await _unitOfWork.Products.GetAllAsync();
            if (products == null)
            {
                throw new Exception("No products found");
            }
            return products;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            Product? product = await _unitOfWork.Products.GetAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task UpdateAsync(Guid id, ProductDto productDto)
        {
            Product? product = await _unitOfWork.Products.GetAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            _mapper.Map(productDto, product);

            // Update the product with the new values
            product.ProductName = productDto.ProductName;            
            product.Description = productDto.Description;
            product.ImageUrl = productDto.ImageUrl;           
            product.CategoryId = productDto.CategoryId;


            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
