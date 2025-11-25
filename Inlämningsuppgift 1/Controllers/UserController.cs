using Inlämningsuppgift_1.Dtos;
using Inlämningsuppgift_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inlämningsuppgift_1.Controllers
{
    [Route("api/users")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task <IActionResult> Register([FromBody] RegisterRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest("Username and password required.");

            var created = await _authService.Register(req);
            if (!created) return Conflict("User already exists.");

            return Ok(new { Message = "Registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest("Missing credentials.");

            var token = await _authService.Login(req);
            if (token == null) return Unauthorized("Invalid credentials.");
                       
            return Ok(new { Token = token });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile([FromHeader(Name = "X-Auth-Token")] string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return Unauthorized();

            var userId = await _authService.GetUserIdFromToken(token);
            if (userId == null) return Unauthorized();

            var profile = await _userService.GetProfile(userId.Value);

            return Ok(profile);
        }
    }
}
