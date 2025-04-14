namespace DAL.Models.Products
{
    public class ProductVariantTopping : BaseEntity
    {
        public Guid ProductVariantId { get; set; }
        public Guid ToppingId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public virtual Topping Topping { get; set; }
    }
}
