namespace DAL.Models.Products
{
    public class Topping : BaseEntity
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string Description { get; set; }
    }
}
