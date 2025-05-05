using BAL.Dtos;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Presentation.Controllers.AuthenticationController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }



        // --------- AUTHENTICATION ---------

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { message = "Invalid username, phone number, or password" });
            }

            return Ok(result);
        }

        [HttpPost("register")]        
        public async Task<IActionResult> Register([FromBody] NewRegisterDto model)
        {
            try
            {
                // Log the incoming request for debugging
                _logger.LogInformation($"Register request received: {JsonSerializer.Serialize(model)}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"Invalid model state: {JsonSerializer.Serialize(ModelState)}");
                    return BadRequest(ModelState);
                }

                var result = await _userService.RegisterAsync(model);

                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return StatusCode(500, new { message = "An error occurred during registration", error = ex.Message });
            }
        }

        // --------- USER MANAGEMENT ---------

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDto userDto)
        {
            try
            {
                await _userService.UpdateUserAsync(id, userDto);
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> AddMoney([FromBody] AddMoneyRequest request)
        {
            try
            {
                await _userService.AddMoneyToWalletAsync(request.Id, request.Amount);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        public class AddMoneyRequest
        {
            public Guid Id { get; set; }
            public decimal Amount { get; set; }
        }


    }
}
