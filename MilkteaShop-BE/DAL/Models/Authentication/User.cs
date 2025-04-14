using DAL.Models.Orders;

namespace DAL.Models.Authentication
{
    public class User : BaseEntity
    {
        public required string Account { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required Guid RoleId { get; set; }
        public required UserRole Role { get; set; }
        public decimal? MoneyAmount { get; set; }
        public virtual IEnumerable<Order>? Orders { get; set; }
    }
}