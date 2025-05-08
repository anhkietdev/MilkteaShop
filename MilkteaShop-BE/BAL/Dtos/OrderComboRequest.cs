using DAL.Models;

namespace BAL.Dtos
{
    public class OrderComboRequest
    {
        public string? OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public Guid? StoreId { get; set; }
        public string? OrderStatus { get; set; } = "Processing";
        public virtual List<Guid>? ComboItems { get; set; } = new List<Guid>();
    }
}
