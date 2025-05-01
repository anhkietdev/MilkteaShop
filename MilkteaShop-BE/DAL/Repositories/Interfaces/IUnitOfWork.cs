namespace DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IOrderRepository Orders { get; }
        public IComboItemRepository ComboItems { get; }
        public ICategoryExtraMappingRepository CategoryExtraMappings { get; }
        public IOrderItemRepository OrderItems { get; }
        public IProductSizeRepository ProductSize { get; }
        public IStoreRepository Stores { get; }

        Task SaveAsync();
    }
}
