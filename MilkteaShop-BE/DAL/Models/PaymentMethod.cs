namespace DAL.Models
{
    public class PaymentMethod : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
