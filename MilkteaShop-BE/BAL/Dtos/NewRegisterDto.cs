using DAL.Models;

namespace BAL.Dtos
{
    public class NewRegisterDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public Role Role { get; set; }
        public Guid? StoreId { get; set; }
    }
}
