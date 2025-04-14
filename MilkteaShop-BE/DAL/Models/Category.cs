namespace DAL.Models
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
