namespace DAL.Models
{
    public class ComboItem : BaseEntity
    {
        public required string ComboCode { get; set; }
        public string? Description { get; set; }
        public required virtual List<ProductSize> ProductSizes { get; set; } = new();
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
