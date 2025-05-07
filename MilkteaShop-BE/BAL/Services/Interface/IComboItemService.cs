using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IComboItemService
    {
        // Fetch all combo items with related data as DTOs
        Task<ICollection<ComboItemResponeDto>> GetAllComboItemAsync();

        // Fetch a single combo item by its ID as DTO
        Task<ComboItemResponeDto> GetComboItemByIdAsync(Guid id);

        // Create a new combo item using ComboItemDto
        Task CreateComboItemAsync(ComboItemDto comboItemDto);

        // Update an existing combo item by ID
        Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto);

        // Delete a combo item by ID
        Task<bool> DeleteComboItemAsync(Guid id);
    }
}
