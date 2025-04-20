using BAL.Dtos;

namespace BAL.Services.Interface
{
    public interface IUserService
    {
        Task<LoginResultDto> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
    }
}
