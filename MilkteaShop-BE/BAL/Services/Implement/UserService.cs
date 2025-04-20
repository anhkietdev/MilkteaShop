using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto loginDto)
        {
            var result = await _unitOfWork.Users.GetAsync(
                u => u.Username == loginDto.Username && u.PasswordHash== loginDto.Password,
                tracked: false
            );

            if (result == null)
            {
                return new LoginResultDto
                {
                    IsSuccess = false,                    
                };
            }
            var token = _jwtTokenGenerator.GenerateToken(
    result.UserId.ToString(),
    result.User.FullName,
    result.User.Email ?? string.Empty,
    result.Role.ToString()
);


        }
        public Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
