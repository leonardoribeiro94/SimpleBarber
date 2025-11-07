using Microsoft.AspNetCore.Identity;

namespace SimpleBarber.Api.Services;

public class IdentityService
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly IConfiguration _configuration;

    public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return result;
    }
    
    public async Task SignInAsync(IdentityUser user)
    {
        await _signInManager.SignInAsync(user, false);
    }

    public async Task<SignInResult> LoginAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        
        return result;
    }
}