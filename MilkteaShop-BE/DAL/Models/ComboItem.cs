namespace DAL.Models
{
    public class ComboItem : BaseEntity
    {
        public required string ComboCode { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ComboItemProductSize> ComboItemProductSizes { get; set; } = new List<ComboItemProductSize>();
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
