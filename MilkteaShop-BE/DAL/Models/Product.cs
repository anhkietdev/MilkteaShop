namespace DAL.Models
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required int Size { get; set; }
        public required Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        public IEnumerable<Topping> Toppings { get; set; } = new List<Topping>();
    }
}
