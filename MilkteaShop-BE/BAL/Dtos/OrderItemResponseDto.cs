namespace BAL.Dtos
{
    public class OrderItemResponseDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductSizeId { get; set; }
        public ProductSizeResponseDto? ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid? ParentOrderItemId { get; set; }
        public OrderItemResponseDto? ParentOrderItem { get; set; }
        public ICollection<OrderItemResponseDto>? ToppingItems { get; set; }
    }
}
