namespace BAL.Dtos
{
    public class ComboItemDto
    {
        public Guid Id { get; set; }
        public string ComboCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ComboProductDto> Products { get; set; } = new List<ComboProductDto>();
    }
}