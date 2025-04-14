using DAL.Models.UserModels;
using Domain.Entities;

namespace DAL.Models.OrderModels
{
    public class Order : BaseEntity
    {
        public required decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public required IEnumerable<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
