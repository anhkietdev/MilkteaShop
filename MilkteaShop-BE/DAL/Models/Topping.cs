namespace DAL.Models
{
    public class Topping : BaseEntity
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}
