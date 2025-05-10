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
                includeProperties: "Store",
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
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                Id = user.Id,
                StoreId = user.StoreId,
            };
        }

        public async Task<ICollection<User>> GetAllUserAsync()
        {
            ICollection<User> users = await _unitOfWork.Users.GetAllAsync();
            if (users == null)
            {
                throw new Exception("No User found");
            }
            return users;
        }



        public async Task<AuthenResultDto> RegisterAsync(NewRegisterDto userDto)
        {
            try
            {
                // Validate the input
                if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password) ||
                    string.IsNullOrEmpty(userDto.PhoneNumber))
                {
                    return new AuthenResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Required fields are missing"
                    };
                }

                // Check if user already exists
                var existingUser = await _unitOfWork.Users.GetAsync(
                    u => u.Username == userDto.Username || u.PhoneNumber == userDto.PhoneNumber,
                    tracked: false);

                if (existingUser != null)
                {
                    return new AuthenResultDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Username or phone number already in use"
                    };
                }

                // Create new user with proper password hashing
                User newUser = new User
                {
                    Username = userDto.Username,
                    // TODO: Implement proper password hashing instead of storing plaintext
                    PasswordHash = userDto.Password, // Consider using BCrypt or other hashing library
                    PhoneNumber = userDto.PhoneNumber,
                    Email = userDto.Email,
                    ImageUrl = userDto.ImageUrl,
                    Role = userDto.Role,
                    StoreId = userDto.StoreId,
                };

                await _unitOfWork.Users.AddAsync(newUser);
                await _unitOfWork.SaveAsync();

                var secretKey = _configuration["JwtSettings:SecretKey"];
                var issuer = _configuration["JwtSettings:Issuer"];
                var audience = _configuration["JwtSettings:Audience"];
                string token = JwtGenerator.GenerateToken(newUser, secretKey, 1000000, issuer, audience);

                return new AuthenResultDto
                {
                    Token = token,
                    IsSuccess = true,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    PhoneNumber = newUser.PhoneNumber,
                    ImageUrl = newUser.ImageUrl,
                    Role = newUser.Role.ToString()
                };
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Registration failed: {ex.Message}");

                return new AuthenResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Registration failed. Please try again."
                };
            }
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
