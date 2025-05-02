namespace BAL.Dtos
{
    public class OrderItemToppingDto
    {
        public Guid ToppingProductSizeId { get; set; }
        public ProductSizeResponseDto? ToppingProductSize { get; set; }
    }
}
