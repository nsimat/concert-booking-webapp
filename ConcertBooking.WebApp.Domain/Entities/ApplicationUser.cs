namespace ConcertBooking.WebApp.Domain.Entities;

// One-to-many relationship with Booking: A customer (1) -----> (*) Bookings
public class ApplicationUser //Customer/user/Spectator: Faut-il utiliser IdentityUser ou créer ma propre classe?
{
    public Guid ApplicationUserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? RefreshToken { get; set; }
    public string? Role { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
    
    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}
// Rôle métier: Identify the customer and his rights on the show tickets.
