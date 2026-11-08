using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Domain.Entities;

// A Venue:   (1) ------> (*)(Concert)
// An Artist: (1) ------> (*)(Concert)
// A Concert: (1) ------> (*)(Booking)
// A Ticket:  (1) ------> (1)(Concert)
public class Concert : AuditableEntity // Event
{
    public string Title { get; set; } = string.Empty;// Name
    public string? Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public ConcertStatus Status { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public int AvailableSeats { get; set; } // Number of available seats for the concert. Une règle « places disponibles » doit-elle venir ici?
    public Guid VenueId { get; set; } // working as a foreign key
    public virtual Venue Venue { get; set; } = null!;
    public Guid ArtistId { get; set; } // working as a foreign key
    public virtual Artist Artist { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
}
// Rôle métier/ Job role: define the event, its date, and its status. It links the artist to the venue and manages the global availability logic (e.g., determining if the concert is sold out). It also manages the concert status (scheduled, postponed, sold out, canceled).