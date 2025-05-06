using BAL.Dtos;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interface
{
    public interface IStoreService
    {
        Task<ICollection<StoreResponeDto>> GetAllStoreAsync();
        Task<StoreResponeDto> GetStoreByIdAsync(Guid id);
        Task CreateStoreAsync(StoreDto storeDto);
        Task UpdateStoreAsync(Guid id, StoreDto storeDto);
        Task<bool> DeleteStoreAsync(Guid id);
    }
}
