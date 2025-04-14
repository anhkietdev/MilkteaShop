namespace DAL.Models.UserModels
{
    public class Role
    {
        public required string RoleName { get; set; }
        public IEnumerable<User>? Users { get; set; }
    }
}
