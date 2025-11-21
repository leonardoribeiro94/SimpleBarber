using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;

namespace SimpleBarber.Api.Services;

public class AuthService
{
    private readonly UserRepository _userRepository;
    private readonly JwtService _jwtService;

    public AuthService(UserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<User?> RegisterAsync(UserDto dto, string role = "Customer")
    {
        if (dto.Password != dto.ConfirmPassword)
            throw new InvalidOperationException("Senha e confirmação não conferem.");

        var existing = await _userRepository.GetByLoginAsync(dto.Email);
        if (existing is not null)
            throw new InvalidOperationException("Já existe um usuário com esse login.");

        var saltHash = BCrypt.Net.BCrypt.GenerateSalt(6);

        var user = new User
        {
            Login = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, saltHash),
            Role = role
        };

        await _userRepository.CreateAsync(user);
        return user;
    }

    public async Task<UserTokenDto?> LoginAsync(string login, string password)
    {
        var user = await _userRepository.GetByLoginAsync(login);
        if (user is null)
            return null;

        var isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!isValid)
            return null;

        return _jwtService.GenerateToken(user);
    }
}