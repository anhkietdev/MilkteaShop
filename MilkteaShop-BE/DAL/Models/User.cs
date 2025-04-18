﻿namespace DAL.Models
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public Role Role { get; set; }
    }
}
