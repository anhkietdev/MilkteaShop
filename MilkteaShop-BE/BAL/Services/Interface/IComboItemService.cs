using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IComboItemService
    {
        Task<IEnumerable<ComboItemResponeDto>> GetAllComboItemAsync();

        Task<ComboItemResponeDto> GetComboItemByIdAsync(Guid id);

        Task CreateComboItemAsync(ComboItemDto comboItemDto);

        Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto);

        Task<bool> DeleteComboItemAsync(Guid id);
    }
}
