using AutoMapper;
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
        
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;  
            _configuration = configuration;
        }

        public async Task<AuthenResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetAsync(
                u => (u.Username == loginDto.Username || u.PhoneNumber == loginDto.PhoneNumber)
                     && u.PasswordHash == loginDto.Password,
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
               
                    Username = user.Username,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    Role = user.Role.ToString()
                
            };
        }

        public async Task<ICollection<User>> GetAllUserAsync()
        {
            ICollection<User> users = await _unitOfWork.Users.GetAllAsync();
            if (users== null)
            {
                throw new Exception("No User found");
            }
            return users;
        }



        public async Task<User> RegisterAsync(RegisterDto registerDto)
        {

            User user = _mapper.Map<User>(registerDto);
            //var newUser = new User
            //{
            //    Username = registerDto.Username,
            //    PasswordHash = registerDto.Password,
            //    PhoneNumber = registerDto.PhoneNumber,
            //    StoreId = registerDto.StoreId,
            //    Role = registerDto.Role,
            //};

            var result = _unitOfWork.Users.AddAsync(user);

            await _unitOfWork.SaveAsync();

            if (user == null)
            {
                throw new Exception("User created failed!");
            }

            //var secretKey = _configuration["JwtSettings:SecretKey"];
            //var issuer = _configuration["JwtSettings:Issuer"];
            //var audience = _configuration["JwtSettings:Audience"];
            //string token = JwtGenerator.GenerateToken(user, secretKey, 1000000, issuer, audience);

            return user;
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            User? users = await _unitOfWork.Users.GetAsync(c => c.Id == id);
            if (users == null)
            {
                throw new Exception("User not found");
            }
            return users;
        }



        public async Task UpdateUserAsync(Guid id, UserDto userDto)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            _mapper.Map(userDto, user);

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.ImageUrl = userDto.ImageUrl;
            user.IsActive = userDto.IsActive;
            user.UpdatedAt = userDto.UpdatedAt;
            user.CreatedAt = userDto.CreatedAt;

            // Safe parsing of Role from string
            if (Enum.TryParse<Role>(userDto.Role, true, out var role)) // true => case-insensitive
            {
                user.Role = role;
            }
            else
            {
                throw new ArgumentException($"Invalid role value: {userDto.Role}");
            }

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> AddMoneyToWalletAsync(Guid id, decimal amount)
        {
            var user = await _unitOfWork.Users.GetAsync(c => c.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            user.WalletBalance += amount;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
