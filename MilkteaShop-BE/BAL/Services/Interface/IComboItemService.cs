using BAL.Dtos;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interface
{
    public interface IComboItemService {
        Task<ICollection<ComboItem>> GetAllComboItemAsync();
        Task<ComboItem> GetComboItemByIdAsync(Guid id);
        Task CreateComboItemAsync(ComboItemDto comboItemDto);
        Task UpdateComboItemAsync(Guid id, ComboItemDto comboItemDto);
        Task<bool> DeleteComboItemAsync(Guid id);

    }
}
