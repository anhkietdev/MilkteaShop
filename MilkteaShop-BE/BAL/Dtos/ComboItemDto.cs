namespace BAL.Dtos
{
    public class ComboItemDto
    {
        public Guid Id { get; set; }
        public string ComboCode { get; set; } // Required field
        public string? Description { get; set; } // Optional field
        public List<ProductSizeQuantityDto> ProductSizes { get; set; }
        public decimal Price {  get; set; }
    }
}