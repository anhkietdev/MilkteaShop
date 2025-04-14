using DAL.Models.Products;

namespace DAL.Models.Orders
{
    public class OrderItemTopping : BaseEntity
    {
        public Guid OrderItemId { get; set; }
        public Guid ToppingId { get; set; }
        public required decimal UnitPrice { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public virtual Topping Topping { get; set; }
    }
}
