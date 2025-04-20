using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class CategoryExtraMappingRepository : Repository<CategoryExtraMapping>, ICategoryExtraMappingRepository
    {
        public CategoryExtraMappingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
