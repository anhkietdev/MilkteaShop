namespace DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IComboRepository ComboRepository { get; }
        public IComboItemRepository ComboItemRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public IOrderItemToppingRepository OrderItemToppingRepository { get; }
        public IProductVariantRepository ProductVariantRepository { get; }
        public IProductVariantToppingRepository ProductVariantToppingRepository { get; }
        public ISizeRepository SizeRepository { get; }
        public IToppingRepository ToppingRepository { get; }
        Task SaveAsync();
    }
}
