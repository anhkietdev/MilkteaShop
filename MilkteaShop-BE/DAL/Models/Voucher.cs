namespace DAL.Models
{
    public class Voucher : BaseEntity
    {
        public required string VoucherCode { get; set; }
        public decimal PriceCondition { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ExceedDate { get; set; }
        public List<Order>? OrderList { get; set; }
        public static string GenerateVoucherNumber()
        {
            var random = new Random();
            var randomNumber = random.Next(100000, 999999);
            var voucherPrefix = "VOU";
            var orderNumber = $"{voucherPrefix}-{DateTime.Now:yyyyMMdd}-{randomNumber}";
            return orderNumber;
        }
    }
}
