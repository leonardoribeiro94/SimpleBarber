using Microsoft.AspNetCore.Identity;
using SimpleBarber.Api.Infrastructure.Repositories;

namespace SimpleBarber.Api.Extensions.cs;

public static class IdentityExtensions
{
    public static void AddCustomIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}