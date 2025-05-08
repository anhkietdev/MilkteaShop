namespace BAL.Dtos
{
    public class CategoryExtraMappingResponseDto
    {
        public Guid Id { get; set; }
        public Guid? MainCategoryId { get; set; }
        public Guid? ExtraCategoryId { get; set; }
        public string? MainCategoryName { get; set; }
        public string? ExtraCategoryName { get; set; }
        public string? MainCategoryDescription { get; set; }
        public string? ExtraCategoryDescription { get; set; }
    }
}
