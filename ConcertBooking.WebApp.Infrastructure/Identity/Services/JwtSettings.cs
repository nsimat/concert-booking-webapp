namespace ConcertBooking.WebApp.Infrastructure.JWT;

public sealed class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public required string Secret { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public int ExpiryInMinutes { get; init; } = 15;
    public int RefreshTokenLifetimeDays { get; init; } = 7;
}