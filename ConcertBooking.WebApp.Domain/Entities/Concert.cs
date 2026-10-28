using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Domain.Entities;

// A Venue: (1) ------> (*)(Concert)
// An Artist: (1) ------> (*)(Concert)
// A Concert: (1) -----> (*)(Booking)
public class Concert : AuditableEntity // Event
{
    public required string Title { get; set; } = string.Empty;// Name
    public string? Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public ConcertStatus Status { get; set; }
    
    [Required]
    public DateTime DateTime { get; set; }
    
    // Navigation
    [Required]
    [ForeignKey("VenueId")]
    public Guid VenueId { get; set; } // working as a foreign key
    public virtual Venue Venue { get; set; } = null!;
    
    // Navigation
    [Required]
    [ForeignKey("ArtistId")]
    public Guid ArtistId { get; set; } // working as a foreign key
    public virtual Artist Artist { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}
// Rôle métier/ Job role: define the event, its date, and its status. It links the artist to the venue and manages the global availability logic (e.g., determining if the concert is sold out). It also manages the concert status (scheduled, postponed, sold out, canceled).