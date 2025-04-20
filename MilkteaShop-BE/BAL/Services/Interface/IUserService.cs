using BAL.Dtos;

namespace BAL.Services.Interface
{
    public interface IUserService
    {
        Task<AuthenResultDto> LoginAsync(LoginDto loginDto);
        Task<AuthenResultDto> RegisterAsync(RegisterDto registerDto);
    }
}
