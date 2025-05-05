using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public ComboItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<ComboItemDto>> GetAllComboItemAsync()
        {
            // Fetch combo items including related product sizes
            var comboItems = await _unitOfWork.ComboItems
                .GetAllAsync(includeProperties: "ComboItemProductSizes.ProductSize");

            if (comboItems == null || !comboItems.Any())
            {
                throw new Exception("No combo items found");
            }

            // Map the entities to DTOs
            var comboItemDtos = comboItems.Select(comboItem => new ComboItemDto
            {
                ComboCode = comboItem.ComboCode, // Required field
                Description = comboItem.Description, // Optional field
                ProductSizeIds = comboItem.ComboItemProductSizes
                    .Select(cips => cips.ProductSize.Id) // Extract ProductSize Ids
                    .ToList(),
                Quantity = comboItem.Quantity, // The quantity of the item
                Price = comboItem.Price // The price of the item
            }).ToList();

            return comboItemDtos;
        }


        public async Task<ComboItem> GetComboItemByIdAsync(Guid id)
        {
            var comboItem = await _unitOfWork.ComboItems
                .GetAsync(
                    c => c.Id == id,
                    includeProperties: "ComboItemProductSizes.ProductSize");

            if (comboItem == null)
            {
                throw new Exception("Combo item not found");
            }
            return comboItem;
        }

        public async Task CreateComboItemAsync(ComboItemDto comboItemDto)
        {
            // Validate input
            if (comboItemDto == null)
            {
                throw new ArgumentNullException(nameof(comboItemDto));
            }

            // Check if product sizes exist
            var existingProductSizes = await _unitOfWork.ProductSize
                .GetAllAsync(ps => comboItemDto.ProductSizeIds.Contains(ps.Id));

            var missingIds = comboItemDto.ProductSizeIds
                .Except(existingProductSizes.Select(ps => ps.Id))
                .ToList();

            if (missingIds.Any())
            {
                throw new KeyNotFoundException($"The following ProductSize IDs were not found: {string.Join(", ", missingIds)}");
            }

            // Map and create combo item
            var comboItem = _mapper.Map<ComboItem>(comboItemDto);
            comboItem.ComboItemProductSizes = existingProductSizes
                .Select(ps => new ComboItemProductSize
                {
                    ProductSizeId = ps.Id,
                    ComboItem = comboItem
                })
                .ToList();

            await _unitOfWork.ComboItems.AddAsync(comboItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto)
        {
            // Validate input
            if (comboItemDto == null)
            {
                throw new ArgumentNullException(nameof(comboItemDto));
            }

            // Get existing combo item with relationships
            var comboItem = await _unitOfWork.ComboItems
                .GetAsync(
                    c => c.Id == id,
                    includeProperties: "ComboItemProductSizes");

            if (comboItem == null)
            {
                throw new Exception("ComboItem not found");
            }

            // Check if product sizes exist
            var existingProductSizes = await _unitOfWork.ProductSize
                .GetAllAsync(ps => comboItemDto.ProductSizeIds.Contains(ps.Id));

            var missingIds = comboItemDto.ProductSizeIds
                .Except(existingProductSizes.Select(ps => ps.Id))
                .ToList();

            if (missingIds.Any())
            {
                throw new KeyNotFoundException($"The following ProductSize IDs were not found: {string.Join(", ", missingIds)}");
            }

            // Update basic properties
            _mapper.Map(comboItemDto, comboItem);

            // Update relationships
            var currentProductSizeIds = comboItem.ComboItemProductSizes
                .Select(cps => cps.ProductSizeId)
                .ToList();

            var productSizesToAdd = existingProductSizes
                .Where(ps => !currentProductSizeIds.Contains(ps.Id))
                .ToList();

            var productSizesToRemove = comboItem.ComboItemProductSizes
                .Where(cps => !comboItemDto.ProductSizeIds.Contains(cps.ProductSizeId))
                .ToList();

            // Remove old relationships
            foreach (var item in productSizesToRemove)
            {
                comboItem.ComboItemProductSizes.Remove(item);
            }

            // Add new relationships
            foreach (var productSize in productSizesToAdd)
            {
                comboItem.ComboItemProductSizes.Add(new ComboItemProductSize
                {
                    ProductSizeId = productSize.Id,
                    ComboItem = comboItem
                });
            }

            await _unitOfWork.ComboItems.UpdateAsync(comboItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteComboItemAsync(Guid id)
        {
            var comboItem = await _unitOfWork.ComboItems.GetAsync(c => c.Id == id);
            if (comboItem == null)
            {
                return false;
            }

            await _unitOfWork.ComboItems.RemoveAsync(comboItem);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}