using DAL.Models.Product;
using DAL.Models.Promotion;

namespace DAL.Models.Orders
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }

        public Guid? ProductVariantId { get; set; } // Nullable vì có thể là combo
        public Guid? ComboId { get; set; } // Nullable vì có thể là sản phẩm đơn

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public virtual Combo Combo { get; set; }

        public virtual ICollection<OrderItemTopping> OrderItemToppings { get; set; }
    }
}
