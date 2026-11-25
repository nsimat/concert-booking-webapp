namespace ConcertBooking.WebApp.Persistence.Identity;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string TokenHash { get; set; } = string.Empty; // Never the token in clair
    public DateTimeOffset ExpiresAt { get; set; }
    private bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public bool IsActive => RevokedAt == null && !IsExpired;
}