using DAL.Models;

namespace BAL.Dtos
{
    public record RegisterDto(string Username, string Password, string PhoneNumber, Guid StoreId, Role Role);
}
