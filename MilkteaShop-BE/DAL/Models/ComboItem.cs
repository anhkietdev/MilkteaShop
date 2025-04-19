namespace DAL.Models
{
    public class ComboItem : BaseEntity
    {
        public required string ComboCode { get; set; }
        public string? Description { get; set; }
        public Guid ProductId { get; set; }
        public required virtual Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
