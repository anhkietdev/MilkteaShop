namespace BAL.Dtos
{
    public class OrderItemRequestDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductSizeId { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Guid>? ToppingItems { get; set; } 
    }
}
