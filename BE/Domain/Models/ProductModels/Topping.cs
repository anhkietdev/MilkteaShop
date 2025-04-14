using Domain.Entities;

namespace DAL.Models.ProductModels
{
    public class Topping : BaseEntity
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}
