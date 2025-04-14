namespace DAL.Models.Orders
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }        
        public required PaymentMethod Method { get; set; }
        public required decimal Amount { get; set; }        
        public string TransactionId { get; set; }
        public PaymentStatus Status { get; set; }
        public virtual Order Order { get; set; }
    }
}
