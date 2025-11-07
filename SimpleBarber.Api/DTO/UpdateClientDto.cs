namespace SimpleBarber.Api.DTO;

public record UpdateClientDto(string Name, string Phone, string Email, DateOnly BirthDate);