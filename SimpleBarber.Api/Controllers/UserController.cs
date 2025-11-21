using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;
using SimpleBarber.Api.Services;

namespace SimpleBarber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserController(UserRepository userRepository,
            JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if (userDto.Password != userDto.ConfirmPassword)
                return BadRequest("The passwords do not match.");

            var user = new User()
            {
                Login = userDto.Email,
                PasswordHash = userDto.Password
            };

            await _userRepository.CreateAsync(user);
            return Ok(_jwtService.GenerateToken(user));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> GetAll()
        {
            var response = await _userRepository.GetAll();
            return Ok(response);
        }

        [HttpGet("string:email")]
        public async Task<IActionResult> GetByLogin(string email)
        {
            var response = await _userRepository.GetByLoginAsync(email);

            return Ok(response);
        }
    }
}