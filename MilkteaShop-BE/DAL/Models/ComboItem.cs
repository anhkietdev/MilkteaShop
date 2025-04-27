namespace DAL.Models
{
    public class ComboItem : BaseEntity
    {
        public required string ComboCode { get; set; }
        public string? Description { get; set; }
        public Guid ProductSizeId { get; set; }
        public required virtual ProductSize ProductSize { get; set; }
        public int Quantity { get; set; }

    }
}
