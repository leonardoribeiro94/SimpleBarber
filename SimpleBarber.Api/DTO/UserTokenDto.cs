namespace SimpleBarber.Api.DTO;

public record UserTokenDto (bool Authenticated, DateTime Expiration, string Token, string Message);