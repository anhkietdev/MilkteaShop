   namespace DAL.Models
{
    public class Category : BaseEntity
    {
        public required string CategoryName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<CategoryExtraMapping>? CategoryExtraMappings { get; set; }
        public virtual ICollection<CategoryExtraMapping>? CategoryMainMappings { get; set; }

    }
}
