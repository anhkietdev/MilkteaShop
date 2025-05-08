using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<ComboItemService> _logger;  // Inject logger

        public ComboItemService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ComboItemService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;  // Initialize logger
        }

        public async Task<ICollection<ComboItemResponeDto>> GetAllComboItemAsync()
        {
            var comboItems = await _unitOfWork.ComboItems
                .GetAllAsync(includeProperties: "ComboItemProductSizes.ProductSize.Product");

            if (comboItems == null || !comboItems.Any())
            {
                _logger.LogWarning("No combo items found in the database.");
                throw new Exception("No combo items found in the database.");
            }

            return comboItems.Select(comboItem => new ComboItemResponeDto
            {
                Id = comboItem.Id,
                ComboCode = comboItem.ComboCode,
                Description = comboItem.Description,
                Price = comboItem.Price,
                ProductSizes = comboItem.ComboItemProductSizes?.Select(cips => new ProductSizeResponseDto
                {
                    Id = cips.ProductSize?.Id ?? Guid.Empty,
                    ProductId = cips.ProductSize?.ProductId ?? Guid.Empty,
                    ProductName = cips.ProductSize?.Product?.ProductName ?? "Unknown",
                    Size = cips.ProductSize.Size,
                    Price = cips.ProductSize?.Price ?? 0,
                    Quantity = cips.Quantity  // Include Quantity in the response
                }).ToList() ?? new List<ProductSizeResponseDto>()
            }).ToList();
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
                ProductSizes = comboItem.ComboItemProductSizes?.Select(cips => new ProductSizeResponseDto
                {
                    Id = cips.ProductSize?.Id ?? Guid.Empty,
                    ProductId = cips.ProductSize?.ProductId ?? Guid.Empty,
                    ProductName = cips.ProductSize?.Product?.ProductName ?? "Unknown",
                    Size = cips.ProductSize.Size,
                    Price = cips.ProductSize?.Price ?? 0,
                    Quantity = cips.Quantity  // Include Quantity in the response
                }).ToList() ?? new List<ProductSizeResponseDto>()
            };
        }

        public async Task CreateComboItemAsync(ComboItemDto comboItemDto)
        {
            if (comboItemDto == null)
            {
                _logger.LogError("Received null ComboItemDto.");
                throw new ArgumentNullException(nameof(comboItemDto));
            }

            // Validate ProductSizes and Quantity
            if (comboItemDto.ProductSizes == null || comboItemDto.ProductSizes.Count < 2)
            {
                _logger.LogError("Combo item requires at least two ProductSize entries.");
                throw new ArgumentException("At least two ProductSize entries must be provided.", nameof(comboItemDto.ProductSizes));
            }

            // Check for duplicate ProductSizeIds
            var duplicateProductSizeIds = comboItemDto.ProductSizes
                .GroupBy(x => x.ProductSizeId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateProductSizeIds.Any())
            {
                _logger.LogError($"Duplicate ProductSizeIds found: {string.Join(", ", duplicateProductSizeIds)}");
                throw new ArgumentException($"The following ProductSizeIds are duplicated: {string.Join(", ", duplicateProductSizeIds)}");
            }

            // Validate that Quantity and Price are greater than 0
            foreach (var productSize in comboItemDto.ProductSizes)
            {
                if (productSize.Quantity <= 0)
                {
                    _logger.LogError($"Invalid quantity for ProductSizeId '{productSize.ProductSizeId}', quantity must be greater than 0.");
                    throw new ArgumentException($"Quantity must be greater than 0 for ProductSizeId '{productSize.ProductSizeId}'.");
                }
            }

            if (comboItemDto.Price <= 0)
            {
                _logger.LogError("Invalid price for ComboItem, price must be greater than 0.");
                throw new ArgumentException("Price must be greater than 0.");
            }

            var existingProductSizes = await _unitOfWork.ProductSize
                .GetAllAsync(ps => comboItemDto.ProductSizes.Select(x => x.ProductSizeId).Contains(ps.Id));

            var missingIds = comboItemDto.ProductSizes
                .Select(x => x.ProductSizeId)
                .Except(existingProductSizes.Select(ps => ps.Id))
                .ToList();

            if (missingIds.Any())
            {
                _logger.LogError($"The following ProductSize IDs were not found: {string.Join(", ", missingIds)}");
                throw new KeyNotFoundException($"The following ProductSize IDs were not found: {string.Join(", ", missingIds)}");
            }

            var comboItem = _mapper.Map<ComboItem>(comboItemDto);

            // Create ComboItemProductSizes with Quantity
            comboItem.ComboItemProductSizes = comboItemDto.ProductSizes
                .Select(ps => new ComboItemProductSize
                {
                    ProductSizeId = ps.ProductSizeId,
                    ComboItem = comboItem,
                    Quantity = ps.Quantity  // Add Quantity
                }).ToList();

            await _unitOfWork.ComboItems.AddAsync(comboItem);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"Combo item with ID '{comboItem.Id}' created successfully.");
        }

        public async Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid ComboItem ID (empty GUID).", nameof(id));

            if (comboItemDto == null)
                throw new ArgumentNullException(nameof(comboItemDto));

            // Require at least 2 product size entries
            if (comboItemDto.ProductSizes == null || comboItemDto.ProductSizes.Count < 2)
                throw new ArgumentException("At least two ProductSize entries must be provided.", nameof(comboItemDto.ProductSizes));

            // Check for duplicate ProductSizeId in the list
            var duplicateProductSizeIds = comboItemDto.ProductSizes
                .GroupBy(x => x.ProductSizeId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateProductSizeIds.Any())
                throw new ArgumentException($"Duplicate ProductSizeIds found: {string.Join(", ", duplicateProductSizeIds)}");

            // Validate that all quantities are greater than 0
            foreach (var ps in comboItemDto.ProductSizes)
            {
                if (ps.Quantity <= 0)
                    throw new ArgumentException($"Quantity must be greater than 0 for ProductSizeId '{ps.ProductSizeId}'.");
            }

            if (comboItemDto.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            var existingComboItem = await _unitOfWork.ComboItems
                .GetAsync(c => c.Id == id, includeProperties: "ComboItemProductSizes");

            if (existingComboItem == null)
                throw new KeyNotFoundException($"ComboItem with ID {id} was not found.");

            var existingProductSizes = await _unitOfWork.ProductSize
                .GetAllAsync(ps => comboItemDto.ProductSizes.Select(s => s.ProductSizeId).Contains(ps.Id));

            var missingIds = comboItemDto.ProductSizes
                .Select(s => s.ProductSizeId)
                .Except(existingProductSizes.Select(ps => ps.Id))
                .ToList();

            if (missingIds.Any())
                throw new KeyNotFoundException($"The following ProductSize IDs were not found: {string.Join(", ", missingIds)}");

            // Map scalar fields
            existingComboItem.ComboCode = comboItemDto.ComboCode;
            existingComboItem.Description = comboItemDto.Description;
            existingComboItem.Price = comboItemDto.Price;

            // Update ComboItemProductSizes with Quantity
            foreach (var ps in comboItemDto.ProductSizes)
            {
                var existingProductSize = existingComboItem.ComboItemProductSizes
                    .FirstOrDefault(x => x.ProductSizeId == ps.ProductSizeId);

                if (existingProductSize != null)
                {
                    // Update quantity if ProductSize already exists
                    existingProductSize.Quantity = ps.Quantity;
                }
                else
                {
                    // Add new ComboItemProductSize if not found
                    existingComboItem.ComboItemProductSizes.Add(new ComboItemProductSize
                    {
                        ComboItemId = existingComboItem.Id,
                        ProductSizeId = ps.ProductSizeId,
                        Quantity = ps.Quantity  // Add Quantity
                    });
                }
            }

            // Remove ComboItemProductSizes that are not in the updated list
            var productSizeIdsToRemove = existingComboItem.ComboItemProductSizes
                .Where(x => !comboItemDto.ProductSizes.Select(s => s.ProductSizeId).Contains(x.ProductSizeId))
                .ToList();

            foreach (var itemToRemove in productSizeIdsToRemove)
            {
                existingComboItem.ComboItemProductSizes.Remove(itemToRemove);
            }

            await _unitOfWork.ComboItems.UpdateAsync(existingComboItem);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"Combo item with ID '{id}' updated successfully.");
        }

        public async Task<bool> DeleteComboItemAsync(Guid id)
        {
            var comboItem = await _unitOfWork.ComboItems.GetAsync(c => c.Id == id);
            if (comboItem == null)
            {
                _logger.LogWarning($"Combo item with ID '{id}' not found for deletion.");
                return false;
            }

            // Handle cascading delete if needed
            await _unitOfWork.ComboItems.RemoveAsync(comboItem);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"Combo item with ID '{id}' deleted successfully.");
            return true;
        }
    }
}
