using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services.Implement
{
    public class ComboItemService : IComboItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ComboItemService> _logger;

        public ComboItemService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ComboItemService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<ComboItemResponeDto>> GetAllComboItemAsync()
        {
            var comboItems = await _unitOfWork.ComboItems
                .GetAllAsync(includeProperties: "ComboItemProductSizes.ProductSize.Product");

            if (comboItems == null || !comboItems.Any())
            {
                _logger.LogWarning("No combo items found in the database.");
                return Enumerable.Empty<ComboItemResponeDto>();
            }

            return comboItems.Select(comboItem => new ComboItemResponeDto
            {
                Id = comboItem.Id,
                ComboCode = comboItem.ComboCode,
                Description = comboItem.Description,
                Price = comboItem.Price,
                Products = comboItem.ComboItemProductSizes?.Select(cips => new ComboProductResponseDto
                {
                    ProductId = cips.ProductSize?.ProductId ?? Guid.Empty,
                    ProductName = cips.ProductSize?.Product?.ProductName ?? "Unknown",
                    ImageUrl = cips.ProductSize?.Product?.ImageUrl ?? "Unknown",
                    ProductType = cips.ProductSize?.Product.ProductType ?? "Unknown",
                    ProductSize = new ComboProductSizeResponseDto
                    {
                        ProductSizeId = cips.ProductSize?.Id ?? Guid.Empty,
                        Size = cips.ProductSize?.Size?.ToString() ?? "Unknown",
                        Price = cips.ProductSize?.Price ?? 0,
                    }
                }).ToList() ?? new List<ComboProductResponseDto>()
            });
        }
        public async Task<ComboItemResponeDto> GetComboItemByIdAsync(Guid id)
        {
            var comboItem = await _unitOfWork.ComboItems
                .GetAsync(c => c.Id == id, includeProperties: "ComboItemProductSizes.ProductSize.Product");

            if (comboItem == null)
            {
                _logger.LogWarning($"Combo item with ID '{id}' not found.");
                throw new Exception($"Combo item with ID '{id}' not found.");
            }

            return new ComboItemResponeDto
            {
                Id = comboItem.Id,
                ComboCode = comboItem.ComboCode,
                Description = comboItem.Description,
                Price = comboItem.Price,

                Products = comboItem.ComboItemProductSizes?.Select(cips => new ComboProductResponseDto
                {
                    ProductId = cips.ProductSize?.ProductId ?? Guid.Empty,
                    ProductName = cips.ProductSize?.Product?.ProductName ?? "Unknown",
                    ImageUrl = cips.ProductSize?.Product?.ImageUrl ?? "Unknown",  // Assuming the Product has an ImageUrl property

                    ProductSize = new ComboProductSizeResponseDto
                    {
                        ProductSizeId = cips.ProductSize?.Id ?? Guid.Empty,
                        Size = cips.ProductSize?.Size?.ToString() ?? "Unknown",
                        Price = cips.ProductSize?.Price ?? 0,
                    }
                }).ToList() ?? new List<ComboProductResponseDto>()
            };
        }

        public async Task CreateComboItemAsync(ComboItemDto comboItemDto)
        {
            if (comboItemDto == null)
            {
                _logger.LogError("Received null ComboItemDto.");
                throw new ArgumentNullException(nameof(comboItemDto));
            }

            if (comboItemDto.Products == null || !comboItemDto.Products.Any())
            {
                _logger.LogError("Combo item requires at least one Product.");
                throw new ArgumentException("At least one Product must be provided.", nameof(comboItemDto.Products));
            }

            var comboItem = new ComboItem
            {
                ComboCode = comboItemDto.ComboCode,
                Description = comboItemDto.Description,
                Price = comboItemDto.Price,
                ComboItemProductSizes = new List<ComboItemProductSize>()
            };

            await _unitOfWork.ComboItems.AddAsync(comboItem);
            await _unitOfWork.SaveAsync();

            foreach (var productDto in comboItemDto.Products)
            {
                if (productDto == null)
                {
                    _logger.LogWarning("Skipping null ComboProductDto in combo creation.");
                    continue;
                }

                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == productDto.CategoryId);
                if (category == null)
                {
                    _logger.LogWarning($"Category with ID '{productDto.CategoryId}' not found. Skipping product creation.");
                    continue;
                }

                var product = new Product
                {
                    ProductName = productDto.ProductName,
                    Description = productDto.Description,
                    CategoryId = productDto.CategoryId,
                    ImageUrl = productDto.ImageUrl,
                    IsActive = productDto.IsActive,
                    ProductType = "Combo"

                };

                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveAsync();

                // Create product size based on the ComboItemProductSizeDto
                var productSize = new ProductSize
                {
                    ProductId = product.Id,
                    Product = product,
                    Size = productDto.ProductSize.Size,
                    Price = productDto.ProductSize.Price
                };

                await _unitOfWork.ProductSize.AddAsync(productSize);
                await _unitOfWork.SaveAsync();

                // Link the product size to the combo item
                comboItem.ComboItemProductSizes.Add(new ComboItemProductSize
                {
                    ComboItemId = comboItem.Id,
                    ProductSizeId = productSize.Id,
                    Quantity = productDto.ProductSize.Quantity
                });
            }

            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Combo item with ID '{comboItem.Id}' created successfully with products and sizes.");
        }

        public async Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid ComboItem ID.", nameof(id));
            if (comboItemDto == null)
                throw new ArgumentNullException(nameof(comboItemDto));

            var existingComboItem = await _unitOfWork.ComboItems
                .GetAsync(c => c.Id == id, includeProperties: "ComboItemProductSizes");

            if (existingComboItem == null)
                throw new KeyNotFoundException($"ComboItem with ID {id} not found.");

            existingComboItem.ComboCode = comboItemDto.ComboCode;
            existingComboItem.Description = comboItemDto.Description;
            existingComboItem.Price = comboItemDto.Price;

            var existingProductSizes = existingComboItem.ComboItemProductSizes.ToList();
            _unitOfWork.ComboItemProductSize.RemoveRange(existingProductSizes);

            // Ensure changes are saved
            await _unitOfWork.SaveAsync();

            var updatedProductSizeIds = new List<Guid>();

            foreach (var productDto in comboItemDto.Products)
            {
                var sizeEnum = (DAL.Models.Size)productDto.ProductSize.Size;

                Product product;

                if (productDto.ProductId != Guid.Empty)
                {
                    product = await _unitOfWork.Products.GetAsync(p => p.Id == productDto.ProductId);
                    if (product != null)
                    {
                        product.ProductName = productDto.ProductName;
                        product.Description = productDto.Description;
                        product.CategoryId = productDto.CategoryId;
                        product.ImageUrl = productDto.ImageUrl;
                        product.IsActive = productDto.IsActive;
                        product.ProductType = "Combo";

                        await _unitOfWork.Products.UpdateAsync(product);
                        await _unitOfWork.SaveAsync();
                        _logger.LogInformation($"Product ID {productDto.ProductId} updated.");
                    }
                    else
                    {
                        _logger.LogWarning($"Product ID {productDto.ProductId} not found. Creating new product.");
                        var category = await _unitOfWork.Categories.GetAsync(c => c.Id == productDto.CategoryId);
                        if (category == null)
                        {
                            _logger.LogWarning($"Category ID {productDto.CategoryId} not found.");
                            continue;
                        }

                        product = new Product
                        {
                            ProductName = productDto.ProductName,
                            Description = productDto.Description,
                            CategoryId = productDto.CategoryId,
                            ImageUrl = productDto.ImageUrl,
                            IsActive = productDto.IsActive
                        };

                        await _unitOfWork.Products.AddAsync(product);
                        await _unitOfWork.SaveAsync();
                        _logger.LogInformation($"New product created with name: {productDto.ProductName}");
                    }
                }
                else
                {
                    var category = await _unitOfWork.Categories.GetAsync(c => c.Id == productDto.CategoryId);
                    if (category == null)
                    {
                        _logger.LogWarning($"Category ID {productDto.CategoryId} not found.");
                        continue;
                    }

                    product = new Product
                    {
                        ProductName = productDto.ProductName,
                        Description = productDto.Description,
                        CategoryId = productDto.CategoryId,
                        ImageUrl = productDto.ImageUrl,
                        IsActive = productDto.IsActive
                    };

                    await _unitOfWork.Products.AddAsync(product);
                    await _unitOfWork.SaveAsync();
                    _logger.LogInformation($"New product created with name: {productDto.ProductName}");
                }

                var productSize = await _unitOfWork.ProductSize
                    .GetAsync(ps => ps.ProductId == product.Id && ps.Size == sizeEnum);

                if (productSize != null)
                {
                    if (productSize.Price != productDto.ProductSize.Price)
                    {
                        productSize.Price = productDto.ProductSize.Price;
                        await _unitOfWork.ProductSize.UpdateAsync(productSize);
                        _logger.LogInformation($"ProductSize updated for product ID {product.Id} and size {sizeEnum}");
                    }
                }
                else
                {
                    productSize = new ProductSize
                    {
                        ProductId = product.Id,
                        Size = sizeEnum,
                        Price = productDto.ProductSize.Price
                    };

                    await _unitOfWork.ProductSize.AddAsync(productSize);
                    await _unitOfWork.SaveAsync();
                    _logger.LogInformation($"New ProductSize created for product ID {product.Id} and size {sizeEnum}");
                }

                existingComboItem.ComboItemProductSizes.Add(new ComboItemProductSize
                {
                    ComboItemId = existingComboItem.Id,
                    ProductSizeId = productSize.Id,
                    Quantity = productDto.ProductSize.Quantity
                });

                updatedProductSizeIds.Add(productSize.Id);
            }

            await _unitOfWork.ComboItems.UpdateAsync(existingComboItem);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"Combo item '{id}' updated successfully.");
        }


        public async Task<bool> DeleteComboItemAsync(Guid id)
        {
            var comboItem = await _unitOfWork.ComboItems.GetAsync(c => c.Id == id);
            if (comboItem == null)
            {
                _logger.LogWarning($"Combo item with ID '{id}' not found for deletion.");
                return false;
            }

            await _unitOfWork.ComboItems.RemoveAsync(comboItem);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"Combo item with ID '{id}' deleted successfully.");
            return true;
        }
    }
}