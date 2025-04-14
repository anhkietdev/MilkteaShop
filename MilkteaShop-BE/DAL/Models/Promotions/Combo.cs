using DAL.Models.Orders;

namespace DAL.Models.Promotions
{
    public class Combo : BaseEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; } 
        public bool IsPercentage { get; set; } // True nếu DiscountAmount là phần trăm, False nếu là số tiền cố định
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }                
        public virtual ICollection<ComboItem> ComboItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
