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

        public async Task<ICollection<ComboItemResponeDto>> GetAllComboItemAsync()
        {
            var comboItems = await _unitOfWork.ComboItems
                .GetAllAsync(includeProperties: "ComboItemProductSizes.ProductSize.Product");

            if (comboItems == null || !comboItems.Any())
            {
                throw new Exception("No combo items found");
            }

            return comboItems.Select(comboItem => new ComboItemResponeDto
            {
                ComboCode = comboItem.ComboCode,
                Description = comboItem.Description,
                ProductSizes = comboItem.ComboItemProductSizes
                    .Select(cips => new ProductSizeResponseDto
                    {
                        Id = cips.ProductSize.Id,
                        ProductId = cips.ProductSize.ProductId,
                        ProductName = cips.ProductSize.Product.ProductName,
                        Size = cips.ProductSize.Size,
                        Price = cips.ProductSize.Price
                    }).ToList(),
                Price = comboItem.Price
            }).ToList();
        }

        public async Task<ComboItemResponeDto> GetComboItemByIdAsync(Guid id)
        {
            var comboItem = await _unitOfWork.ComboItems
                .GetAsync(c => c.Id == id, includeProperties: "ComboItemProductSizes.ProductSize.Product");

            if (comboItem == null)
            {
                throw new Exception("Combo item not found");
            }

            return new ComboItemResponeDto
            {
                ComboCode = comboItem.ComboCode,
                Description = comboItem.Description,
                ProductSizes = comboItem.ComboItemProductSizes
                    .Select(cips => new ProductSizeResponseDto
                    {
                        Id = cips.ProductSize.Id,
                        ProductId = cips.ProductSize.ProductId,
                        ProductName = cips.ProductSize.Product.ProductName,
                        Size = cips.ProductSize.Size,
                        Price = cips.ProductSize.Price
                    }).ToList(),
                Price = comboItem.Price
            };
        }

        public async Task CreateComboItemAsync(ComboItemDto comboItemDto)
        {
            if (comboItemDto == null)
            {
                throw new ArgumentNullException(nameof(comboItemDto));
            }

            var existingProductSizes = await _unitOfWork.ProductSize
                .GetAllAsync(ps => comboItemDto.ProductSizeIds.Contains(ps.Id));

            var missingIds = comboItemDto.ProductSizeIds
                .Except(existingProductSizes.Select(ps => ps.Id))
                .ToList();

            if (missingIds.Any())
            {
                throw new KeyNotFoundException($"The following ProductSize IDs were not found: {string.Join(", ", missingIds)}");
            }

            var comboItem = _mapper.Map<ComboItem>(comboItemDto);

            comboItem.ComboItemProductSizes = existingProductSizes
                .Select(ps => new ComboItemProductSize
                {
                    ProductSizeId = ps.Id,
                    ComboItem = comboItem
                }).ToList();

            await _unitOfWork.ComboItems.AddAsync(comboItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto)
        {
            if (comboItemDto == null)
            {
                throw new ArgumentNullException(nameof(comboItemDto));
            }

            var existingComboItem = await _unitOfWork.ComboItems
                .GetAsync(c => c.Id == id, includeProperties: "ComboItemProductSizes");

            if (existingComboItem == null)
            {
                throw new KeyNotFoundException($"ComboItem with ID {id} was not found.");
            }

            var existingProductSizes = await _unitOfWork.ProductSize
                .GetAllAsync(ps => comboItemDto.ProductSizeIds.Contains(ps.Id));

            var missingIds = comboItemDto.ProductSizeIds
                .Except(existingProductSizes.Select(ps => ps.Id))
                .ToList();

            if (missingIds.Any())
            {
                throw new KeyNotFoundException($"The following ProductSize IDs were not found: {string.Join(", ", missingIds)}");
            }

            _mapper.Map(comboItemDto, existingComboItem);

            existingComboItem.ComboItemProductSizes.Clear();
            existingComboItem.ComboItemProductSizes = existingProductSizes
                .Select(ps => new ComboItemProductSize
                {
                    ProductSizeId = ps.Id,
                    ComboItemId = existingComboItem.Id
                }).ToList();

            _unitOfWork.ComboItems.UpdateAsync(existingComboItem);
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