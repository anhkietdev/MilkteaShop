using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
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

        public async Task<AuthenResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetAsync(
                u => u.Username == loginDto.Username && u.PasswordHash == loginDto.Password,
                tracked: false
            );

            if (user == null)
            {
                return new AuthenResultDto
                {
                    IsSuccess = false,
                };
            }
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            string token = JwtGenerator.GenerateToken(user, secretKey, 1000000, issuer, audience);
            return new AuthenResultDto
            {
                IsSuccess = true,
                Token = token,
            };        
        }

        public async Task<AuthenResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var newUser = new User
            {
                Username = registerDto.Username,
                PasswordHash = registerDto.Password,
                PhoneNumber = registerDto.PhoneNumber,
            };

            var result = _unitOfWork.Users.AddAsync(newUser);

            await _unitOfWork.SaveAsync();

            if (newUser == null)
            {
                return new AuthenResultDto
                {
                    IsSuccess = false,
                };
            }

            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            string token = JwtGenerator.GenerateToken(newUser, secretKey, 1000000, issuer, audience);

            return new AuthenResultDto
            {
                IsSuccess = true,
                Token = token,
            };
        }
    }
}
