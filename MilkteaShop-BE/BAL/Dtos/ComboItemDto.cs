namespace BAL.Dtos
{
    public class ComboItemDto
    {
        public string ComboCode { get; set; } // Required field
        public string? Description { get; set; } // Optional field
        public List<Guid> ProductSizeIds { get; set; } = new();
        public decimal Price {  get; set; }
    }
}