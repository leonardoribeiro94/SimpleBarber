using Microsoft.EntityFrameworkCore;
using SimpleBarber.Api.Domain;

namespace SimpleBarber.Api.Infrastructure.Repositories;

public class AppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }
    
    public async Task  Update(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        _context.SaveChanges();
    }

    public async Task Delete(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<Appointment> GetById(int id)
    {
        return await _context.Appointments.FindAsync(id);   
    }

    public async Task<IEnumerable<Appointment>> GetAppointments()
    {
        return await _context.Appointments
            .Include(c => c.Customer)
            .Include(s => s.Services)
            .ToListAsync();
    }
}