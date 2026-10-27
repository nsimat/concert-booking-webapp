namespace ConcertBooking.WebApp.Domain.Entities;

public class ApplicationUser
{
    public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}