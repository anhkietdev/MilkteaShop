using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IUserService
    {
        Task<AuthenResultDto> LoginAsync(LoginDto loginDto);
        Task<AuthenResultDto> RegisterAsync(RegisterDto registerDto);
        Task<ICollection<User>> GetAllUserAsync();
    }
}
