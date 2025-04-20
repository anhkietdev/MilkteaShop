using DAL.Context;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; private set; }

        public IProductRepository Products { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IComboItemRepository ComboItems { get; private set; }

        public ICategoryExtraMappingRepository CategoryExtraMappings { get; private set; }

        public IOrderItemRepository OrderItems { get; private set; }

        public IPaymentMethodRepository PaymentMethods { get; private set; }

        public UnitOfWork(AppDbContext context, IUserRepository users, IProductRepository products, ICategoryRepository categories, IOrderRepository orders, IComboItemRepository comboItems, ICategoryExtraMappingRepository categoryExtraMappings, IOrderItemRepository orderItems, IPaymentMethodRepository paymentMethods)
        {
            _context = context;
            Users = new UserRepository(_context);
            Products = products;
            Categories = categories;
            Orders = orders;
            ComboItems = comboItems;
            CategoryExtraMappings = categoryExtraMappings;
            OrderItems = orderItems;
            PaymentMethods = paymentMethods;
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
