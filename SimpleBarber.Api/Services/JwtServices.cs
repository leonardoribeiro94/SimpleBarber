using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Settings;

namespace SimpleBarber.Api.Services;

public class JwtServices
{
    private readonly JwtTokenSettings _jwtTokenSettings;

    public JwtServices(IOptions<JwtTokenSettings> jwtTokenSettings)
    {
        _jwtTokenSettings = jwtTokenSettings.Value;
    }

    public UserTokenDto GenerateToken(UserDto userInfo)
    {
        //Define declarações do usuário
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim("Barberaria", "UsuarioBarbearia"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //gera uma chave com base em um algoritmo simetrico
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtTokenSettings.Key));

        //gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Tempo de expiracão do token. 
        var expiration = DateTime.UtcNow.AddHours(double.Parse(_jwtTokenSettings.ExpireHours.ToString()));

        // classe que representa um token JWT e gera o token
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _jwtTokenSettings.Issuer,
            audience: _jwtTokenSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credenciais);

        //retorna os dados com o token e informacoes
        return new UserTokenDto(true, 
            expiration, 
            new JwtSecurityTokenHandler().WriteToken(token), 
            "Token JWT OK");
    }
}