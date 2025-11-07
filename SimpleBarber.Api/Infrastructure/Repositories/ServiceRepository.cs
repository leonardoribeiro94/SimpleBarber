using Microsoft.EntityFrameworkCore;
using SimpleBarber.Api.Domain;

namespace SimpleBarber.Api.Infrastructure.Repositories;

public class ServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Service service)
    {
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(Service service)
    {
        _context.Services.Remove(service);
        return _context.SaveChangesAsync();
    }

    public async Task<ICollection<Service>> GetServices()
    {
        return await _context.Services.ToListAsync();
    }
    
    public async Task<Service> GetServiceById(int id)
    {
        return await _context.Services.FindAsync(id);
    }
}