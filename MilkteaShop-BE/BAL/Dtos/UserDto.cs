using DAL.Models;

namespace BAL.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public required string PasswordHash { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? StoreId { get; set; }

    }
}