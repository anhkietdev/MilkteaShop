using DAL.Models;

namespace BAL.Dtos
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Size Size { get; set; }
        public bool IsActive { get; set; }
        public string? ProductType { get; set; }
    }
}
