
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Services;

namespace SimpleBarber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly JwtServices _jwtServices;

        public UserController(IdentityService identityService,
        JwtServices jwtServices)
        {
            _identityService = identityService;
            _jwtServices = jwtServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if(userDto.Password != userDto.ConfirmPassword)
                return BadRequest("The passwords do not match.");
            
            var user = new IdentityUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                EmailConfirmed = true
            };
            
            var result = await _identityService.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _identityService.SignInAsync(user);

            return Ok(_jwtServices.GenerateToken(userDto));
        }
    }
}