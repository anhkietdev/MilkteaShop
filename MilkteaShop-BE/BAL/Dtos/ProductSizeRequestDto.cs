using DAL.Models;

namespace BAL.Dtos
{
    public class ProductSizeRequestDto
    {
        public Guid ProductId { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }
}
