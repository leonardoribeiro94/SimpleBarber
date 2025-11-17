using SimpleBarber.Api.Domain;

namespace SimpleBarber.Api.DTO;

public record AppointmentDto(int Id, 
    DateTime DateAppointment, 
    string Notes, 
    int CustomerId, 
    bool Status,
    Customer Customer, 
    ICollection<Service> Services);