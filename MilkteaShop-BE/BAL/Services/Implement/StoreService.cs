using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{

    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public async Task<ICollection<Store>> GetAllStoreAsync()
        //{
        //    var stores = await _unitOfWork.Stores.GetAllAsync(
        //        includeProperties: "Users,Orders",
        //        tracked: false
        //    );

        //    if (stores == null || stores.Count == 0)
        //    {
        //        throw new Exception("No stores found");
        //    }

        //    return stores;
        //}
        public async Task<ICollection<StoreResponeDto>> GetAllStoreAsync()
        {
            string includeProperties = "Users,Orders";
            ICollection<Store> stores = await _unitOfWork.Stores.GetAllAsync(null, includeProperties);
            if (stores == null)
            {
                throw new Exception("No Store found");
            }

            return _mapper.Map<ICollection<StoreResponeDto>>(stores);
        }

        //public async Task<Store> GetStoreByIdAsync(Guid id)
        //    {
        //        Store? stores = await _unitOfWork.Stores.GetAsync(c => c.Id == id);
        //        if (stores == null)
        //        {
        //            throw new Exception("Store not found");
        //        }
        //        return stores;
        //    }
        public async Task<StoreResponeDto> GetStoreByIdAsync(Guid id)
        {
            string includeProperties = "Users,Orders";
            Store? stores = await _unitOfWork.Stores.GetAsync(o => o.Id == id, includeProperties);
            if (stores == null)
            {
                throw new Exception("Store not found");
            }
            return _mapper.Map<StoreResponeDto>(stores);
        }


        public async Task CreateStoreAsync(StoreDto storeDto)
            {
                Store store = _mapper.Map<Store>(storeDto);
                await _unitOfWork.Stores.AddAsync(store);
                await _unitOfWork.SaveAsync();
            }

        public async Task UpdateStoreAsync(Guid id, StoreDto storeDto)
        {
            Store? store = await _unitOfWork.Stores.GetAsync(c => c.Id == id);
            if (store == null)
            {
                throw new Exception("Store not found");
            }
            _mapper.Map(storeDto, store);

            store.StoreName = storeDto.StoreName;
            store.Description = storeDto.Description;
            store.Address = storeDto.Address;
            store.PhoneNumber = storeDto.PhoneNumber;
            store.IsActive = storeDto.IsActive;



            await _unitOfWork.Stores.UpdateAsync(store);
            await _unitOfWork.SaveAsync();
        }


        public async Task<bool> DeleteStoreAsync(Guid id)
        {
            Store? store = await _unitOfWork.Stores.GetAsync(c => c.Id == id);
            if (store == null)
            {
                return false;
            }
            await _unitOfWork.Stores.RemoveAsync(store);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> AddMoneyToCaseBalance(Guid id, decimal money)
        {
            Store? store = await _unitOfWork.Stores.GetAsync(c => c.Id == id);
            if (store == null)
            {
                return false;
            }
            store.CashBalance += money;
            await _unitOfWork.Stores.UpdateAsync(store);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> SubtractMoneyFromCaseBalance(Guid id, decimal money)
        {
            Store? store = await _unitOfWork.Stores.GetAsync(c => c.Id == id);
            if (store == null)
            {
                return false;
            }
            if (store.CashBalance < money)
            {
                return false;
            }
            store.CashBalance -= money;
            await _unitOfWork.Stores.UpdateAsync(store);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }

}
