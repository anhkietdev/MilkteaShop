namespace DAL.Models
{
    public class CategoryExtraMapping : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public required virtual Category Category { get; set; }
    }
}
