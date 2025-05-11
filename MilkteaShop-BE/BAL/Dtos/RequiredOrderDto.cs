namespace BAL.Dtos
{
    public class RequiredOrderDto
    {
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid VoucherId { get; set; }
        public decimal PercentageDiscount { get; set; }
        public decimal AmountDiscount { get; set; }
    }
}
