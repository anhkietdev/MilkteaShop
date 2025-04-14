namespace DAL.Models.Products
{
    public class Size : BaseEntity
    {
        public required string Name { get; set; } // S, M, L, XL...
        public required int VolumeInMl { get; set; } // 300, 500, 700...
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
