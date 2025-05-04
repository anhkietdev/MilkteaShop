namespace DAL.Models
{
    public class OrderItemTopping : BaseEntity
    {
        public Guid OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }

        public Guid ProductSizeId { get; set; }
        public virtual ProductSize ProductSize { get; set; }    
    }
}
