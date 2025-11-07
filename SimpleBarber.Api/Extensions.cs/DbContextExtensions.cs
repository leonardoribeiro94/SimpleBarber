using Microsoft.EntityFrameworkCore;
using SimpleBarber.Api.Infrastructure.Repositories;

namespace SimpleBarber.Api.Extensions.cs;

public static class DbContextExtensions
{
    public static void AddCustomDbContext(this IServiceCollection services, string connectionString)
    { 
        services.AddDbContext<AppDbContext>(opt => { opt.UseSqlServer(connectionString); });
    }
}