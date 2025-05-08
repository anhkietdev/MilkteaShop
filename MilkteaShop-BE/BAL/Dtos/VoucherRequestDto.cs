namespace BAL.Dtos
{
    public class VoucherRequestDto
    {
        public required string VoucherCode { get; set; }
        public decimal PriceCondition { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ExceedDate { get; set; }
        public bool IsActive { get; set; }
    }
}