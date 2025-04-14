using DAL.Models.Promotions;

namespace DAL.Models.Products
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }      
        public required Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        public virtual ICollection<ProductVariant> Variants { get; set; }
        public virtual ICollection<ComboItem> ComboItems { get; set; }
    }
}
