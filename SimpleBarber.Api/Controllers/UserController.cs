using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;
using SimpleBarber.Api.Services;

namespace SimpleBarber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public UserController(AuthService authService,
            UserRepository repository,
            JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            var user = await _authService.RegisterAsync(userDto, userDto.Role);

            if (user == null)
                return BadRequest();

            return Ok(_jwtService.GenerateToken(user));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _repository.GetAll();
            return Ok(response);
        }

        [HttpGet("string:login")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            var response = await _repository.GetByLoginAsync(login);

            return Ok(response);
        }
    }
}