using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;
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

        public async Task<ICollection<ComboItem>> GetAllComboItemAsync()
        {
            ICollection<ComboItem> comboItems = await _unitOfWork.ComboItems.GetAllAsync(includeProperties: "ProductSizes");
            if (comboItems == null || comboItems.Count == 0)
            {
                throw new Exception("No combo items found");
            }
            return comboItems;
        }

        public async Task<ComboItem> GetComboItemByIdAsync(Guid id)
        {
            ComboItem? comboItem = await _unitOfWork.ComboItems.GetAsync(c => c.Id == id, includeProperties: "ProductSizes");
            if (comboItem == null)
            {
                throw new Exception("Combo item not found");
            }
            return comboItem;
        }

        public async Task CreateComboItemAsync(ComboItemDto comboItemDto)
        {
            ComboItem comboItem = _mapper.Map<ComboItem>(comboItemDto);

            // Load related ProductSizes
            var productSizes = await _unitOfWork.ProductSize.GetAllAsync(ps => comboItemDto.ProductSizeIds.Contains(ps.Id));
            comboItem.ProductSizes = productSizes.ToList();
            await _unitOfWork.ComboItems.AddAsync(comboItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto)
        {
            ComboItem? comboItem = await _unitOfWork.ComboItems.GetAsync(c => c.Id == id, includeProperties: "ProductSizes");
            if (comboItem == null)
            {
                throw new Exception("ComboItem not found");
            }

            _mapper.Map(comboItemDto, comboItem);

            // Update many-to-many ProductSizes
            var productSizes = await _unitOfWork.ProductSize.GetAllAsync(ps => comboItemDto.ProductSizeIds.Contains(ps.Id));
            comboItem.ProductSizes = productSizes.ToList();

            comboItem.Description = comboItemDto.Description;
            comboItem.ComboCode = comboItemDto.ComboCode;
            comboItem.Quantity = comboItemDto.Quantity;
            comboItem.Price = comboItemDto.Price;

            await _unitOfWork.ComboItems.UpdateAsync(comboItem);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteComboItemAsync(Guid id)
        {
            ComboItem? comboItem = await _unitOfWork.ComboItems.GetAsync(c => c.Id == id);
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
