using BAL.Dtos;
using DAL.Models;
using System.Threading.Tasks;

namespace BAL.Services.Interface
{
    public interface IUserService
    {
        Task<AuthenResultDto> LoginAsync(LoginDto loginDto);
        Task<User> RegisterAsync(UserDto registerDto);
        Task<ICollection<User>> GetAllUserAsync();
        Task UpdateUserAsync(Guid id, UserDto userDto);
        Task<User> GetUserByIdAsync(Guid id);
        Task<bool> AddMoneyToWalletAsync(Guid id, decimal amount);
    }
}
