using DAL.Models.ProductModels;
using Domain.Entities;

namespace DAL.Models.OrderModels
{
    public class OrderDetail : BaseEntity
    {
        public required Guid ProductId { get; set; }
        public required Product Product { get; set; }
        public required Guid OrderId { get; set; }
        public required Order Order { get; set; }
        public required Guid ToppingId { get; set; }
        public required int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
