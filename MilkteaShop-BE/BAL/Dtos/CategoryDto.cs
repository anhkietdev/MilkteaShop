using DAL.Models;

namespace BAL.Dtos
{
    public class CategoryDto
    {
        public required string CategoryName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<CategoryExtraMapping>? CategoryExtraMappings { get; set; }
    }
}
