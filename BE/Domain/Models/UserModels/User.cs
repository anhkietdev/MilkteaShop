using DAL.Models.OrderModels;
using Domain.Entities;

namespace DAL.Models.UserModels
{
    public class User : BaseEntity 
    {
        public required string Account { get; set; }
        public required string Password { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required Guid RoleId { get; set; }
        public required Role Role { get; set; }
        public decimal? MoneyAmount { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}
