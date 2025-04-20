using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BAL.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetAsync(
                u => u.Username == loginDto.Username && u.PasswordHash == loginDto.Password,
                tracked: false
            );

            if (user == null)
            {
                return new LoginResultDto
                {
                    IsSuccess = false,
                };
            }
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            string token = JwtTokenGenerator.GenerateToken(user, secretKey, 1000000, issuer, audience);
            return new LoginResultDto
            {
                IsSuccess = true,
                Token = token,
            };        
        }

        public Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
