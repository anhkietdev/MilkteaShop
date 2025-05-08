namespace DAL.Models
{
    public class CategoryExtraMapping : BaseEntity
    {

        public Guid MainCategoryId { get; set; }
        public virtual Category MainCategory { get; set; }
        public Guid ExtraCategoryId { get; set; }
        public virtual Category ExtraCategory { get; set; }
    }
}
     