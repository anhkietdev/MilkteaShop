using BAL.Dtos;

namespace BAL.Services.Interface
{
    public interface IStoreService
    {
        Task<ICollection<StoreResponeDto>> GetAllStoreAsync();
        Task<StoreResponeDto> GetStoreByIdAsync(Guid id);
        Task CreateStoreAsync(StoreDto storeDto);
        Task UpdateStoreAsync(Guid id, StoreDto storeDto);
        Task<bool> DeleteStoreAsync(Guid id);
        Task<bool> AddMoneyToCaseBalance(Guid id, decimal money);
        Task<bool> SubtractMoneyFromCaseBalance(Guid id, decimal money);
    }
}
