using BAL.Dtos;
using BAL.Services.Implement;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.AuthenticationController
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

 
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { message = "Invalid username, phone number, or password" });
            }

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }



        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var token = await _userService.RegisterAsync(registerDto);
            if (!token.IsSuccess)
            {
                return BadRequest(new { message = "Registration failed" });
            }
            return Ok(token);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDto userDto)
        {
            try
            {
                await _userService.UpdateUserAsync(id, userDto);
                return NoContent(); // 204 if success
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }



    }
}
