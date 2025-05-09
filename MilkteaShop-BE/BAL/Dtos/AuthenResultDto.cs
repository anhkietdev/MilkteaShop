namespace BAL.Dtos
{
    public class AuthenResultDto
    {
        public string? Token { get; set; }
        public bool IsSuccess { get; set; }
        public Guid? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Role { get; set; }
        public Guid? StoreId { get; set; } 
        public bool IsActive { get; set; } 
        public string? ErrorMessage { get; set; }
    }
}
