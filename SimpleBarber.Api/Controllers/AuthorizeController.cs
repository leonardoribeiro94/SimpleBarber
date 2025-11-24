using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Services;

namespace SimpleBarber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthorizeController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous] 
    public async Task<ActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto.login, dto.password);

        if (token is null)
            Unauthorized("Email or password is incorrect");

        return Ok(token);
    }
}