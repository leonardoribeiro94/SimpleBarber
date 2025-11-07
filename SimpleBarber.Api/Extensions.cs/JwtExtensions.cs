using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SimpleBarber.Api.Extensions.cs;

public static class JwtExtensions
{
    public static void AddCustomJwtAuthentication(this IServiceCollection services, 
        string audience, 
        string issuer, 
        string key)
    {
        services.AddAuthentication( 
                JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key))
                });
    }
}