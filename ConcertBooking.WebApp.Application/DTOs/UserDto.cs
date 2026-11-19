namespace ConcertBooking.WebApp.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? RefreshToken { get; set; }
    public string? Role { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
}