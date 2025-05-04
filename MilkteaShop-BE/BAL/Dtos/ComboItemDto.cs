namespace BAL.Dtos
{
    public class ComboItemDto
    {
        public string ComboCode { get; set; } // Required field
        public string? Description { get; set; } // Optional field
        public Guid ProductSizeId { get; set; } // The Product Size ID
        public int Quantity { get; set; } // The quantity of the item
    }
}