using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleBarber.Api.Settings;

namespace SimpleBarber.Api.Extensions.cs;

public static class JwtExtensions
{
    public static void AddCustomJwtAuthentication(this IServiceCollection services, 
        JwtTokenSettings  settings)
    {
        services.AddAuthentication( 
                JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = settings.Audience,
                    ValidIssuer = settings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(settings.Key))
                });
    }
}