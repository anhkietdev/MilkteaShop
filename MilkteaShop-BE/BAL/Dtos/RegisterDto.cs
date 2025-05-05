using DAL.Models;

namespace BAL.Dtos
{
    public class RegisterDto(string Username, string Password, string PhoneNumber, Guid StoreId, Role Role);
}
