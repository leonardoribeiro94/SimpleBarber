using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Services;

namespace SimpleBarber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly JwtServices _jwtServices;
    private readonly IdentityService _identityService;

    public AuthorizeController(IdentityService identityService,  JwtServices jwtServices)
    {
        _identityService = identityService;
        _jwtServices = jwtServices;
    }

    [HttpPost]
    public async Task<ActionResult> Login([FromBody] UserDto userDto)
    {
        var result = await _identityService.LoginAsync(userDto.Email, userDto.Password);

        if (!result.Succeeded)
            BadRequest("Email or password is incorrect");

        return Ok(_jwtServices.GenerateToken(userDto));
    }
}