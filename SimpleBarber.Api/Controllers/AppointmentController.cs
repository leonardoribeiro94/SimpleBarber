using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;

namespace SimpleBarber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly ServiceRepository _serviceRepository;
    private readonly CustomerRepository _customerRepository;

    public AppointmentController(AppointmentRepository appointmentRepository,
        ServiceRepository serviceRepository,
        CustomerRepository customerRepository)
    {
        _appointmentRepository = appointmentRepository;
        _serviceRepository = serviceRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IActionResult> Get()
    {
        var appointments = await _appointmentRepository.GetAppointments();

        return Ok(appointments);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = _appointmentRepository.GetById(id);

        return Ok(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AppointmentDto dto)
    {
        var appointment = new Appointment()
        {
            CustomerId = dto.CustomerId,
            Notes = dto.Notes,
            DateAppointment = dto.DateAppointment,
            Status = true,
            Customer = await _customerRepository.GetByIdAsync(dto.CustomerId),
        };

        foreach (var serviceDto in dto.Services)
        {
            var service = _serviceRepository.GetServiceById(serviceDto.Id);

            if (service is null)
                continue;

            appointment.Services.Add(serviceDto);
        }

        await _appointmentRepository.Create(appointment);

        return Created(appointment.Id.ToString(), appointment);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] AppointmentDto dto)
    {
        var appointment = await _appointmentRepository.GetById(id);

        appointment.CustomerId = dto.CustomerId;
        appointment.Notes = dto.Notes;
        appointment.DateAppointment = dto.DateAppointment;
        appointment.Status = dto.Status;
        appointment.Customer = await _customerRepository.GetByIdAsync(dto.CustomerId);


        foreach (var serviceDto in dto.Services)
        {
            var service = _serviceRepository.GetServiceById(serviceDto.Id);

            if (service is null)
                continue;

            if (appointment.Services.Any(x => x.Id == serviceDto.Id))
                continue;

            appointment.Services.Add(serviceDto);
        }

        await _appointmentRepository.Update(appointment);
        
        return Ok(appointment);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _appointmentRepository.GetById(id);
        await _appointmentRepository.Delete(appointment);

        return Ok();
    }
}