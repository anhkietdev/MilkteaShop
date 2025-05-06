namespace BAL.Dtos
{
    public class OrderSummaryDto
    {
        public int TotalOrders { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
