using DAL.Models;

namespace BAL.Dtos
{
    public class ProductSizeResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; } 
        public Size? Size { get; set; }
        public decimal Price { get; set; }
    }
}
