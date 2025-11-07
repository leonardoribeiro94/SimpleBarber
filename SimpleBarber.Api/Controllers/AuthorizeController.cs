using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Services;

namespace SimpleBarber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly IdentityService _identityService;
    private readonly JwtServices _jwtServices;

    public AuthorizeController(IdentityService identityService, JwtServices jwtServices)
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

        return Ok(result);
    }
        
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
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