using DAL.Models;

namespace BAL.Dtos
{
    public class VoucherResponseDto
    {
        public Guid Id { get; set; }
        public required string VoucherCode { get; set; }
        public decimal PriceCondition { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ExceedDate { get; set; }
        public List<Order>? OrderList { get; set; }
        public bool IsActive { get; set; }
    }
}
