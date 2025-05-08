namespace DAL.Models
{
    public class Store : BaseEntity
    {
        public string StoreName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<User>? Users { get; set; } = new List<User>();
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
        public decimal CashBalance { get; set; } = 0;
    }
}
