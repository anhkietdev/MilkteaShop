using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IComboItemService
    {
        Task<ICollection<ComboItemDto>> GetAllComboItemAsync();

        Task<ComboItem> GetComboItemByIdAsync(Guid id);

        Task CreateComboItemAsync(ComboItemDto comboItemDto);

        Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto);

        Task<bool> DeleteComboItemAsync(Guid id);
    }
}