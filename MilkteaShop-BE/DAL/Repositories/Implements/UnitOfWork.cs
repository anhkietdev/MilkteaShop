using DAL.Context;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; private set; }

        public IProductRepository Products { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IComboItemRepository ComboItems { get; private set; }

        public ICategoryExtraMappingRepository CategoryExtraMappings { get; private set; }

        public IOrderItemRepository OrderItems { get; private set; }
        public IProductSizeRepository ProductSizes { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Orders = new OrderRepository(_context);
            ComboItems = new ComboItemRepository(_context);
            CategoryExtraMappings = new CategoryExtraMappingRepository(_context);
            OrderItems = new OrderItemRepository(_context);
            ProductSizes = new ProductSizeRepository(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
