using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class ComboItemRepository : Repository<ComboItem>, IComboItemRepository
    {
        public ComboItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
