using DAL.Models.Products;

namespace DAL.Models.Promotions
{
    public class ComboItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid ComboId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Combo Combo { get; set; }
    }
}
