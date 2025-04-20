using BAL.Dtos;
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

        [HttpGet]
        public async Task<IActionResult> Login([FromQuery]LoginDto loginDto)
        {
            var token = await _userService.LoginAsync(loginDto);
            if (!token.IsSuccess)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
            return Ok(token);
        }
    }
}
