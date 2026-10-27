using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Domain.Entities;

// A Venue: (1) ------> (*)(Concert)
// An Artist: (1) ------> (*)(Concert)
// A Concert: (1) -----> (*)(Booking)
public class Concert : AuditableEntity
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateTime { get; set; }
    
    //public ConcertType Type { get; set; }

    //à vérifier la pertinence
    //public int TotalCapacity { get; set; }
    //public int AvailableSeats { get; set; }
    //public decimal TicketPrice { get; set; }

    // Concurrency: prevent the overbooking (optimistic Concurrency)
    //[Timestamp]
    //public byte[] RowVersion { get; set; } = Array.Empty<byte>();// à vérifier
    // fin à vérifier
    
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