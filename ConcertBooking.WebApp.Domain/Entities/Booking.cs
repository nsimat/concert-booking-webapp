using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Domain.Entities;

// Booking (1) -----> (*) Ticket
public class Booking : AuditableEntity
{
    [Required]
    public DateTime BookingDate { get; set; }

    public BookingStatus Status { get; set; }
    
    // Navigation
    [Required]
    [ForeignKey("ConcertId")]
    public Guid ConcertId { get; set; }
    public virtual Concert Concert { get; set; } = null!;
    
    // Navigation
    [Required]
    [ForeignKey("ApplicationUserId")]
    public Guid ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    
    public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
}
//Rôle métier: Contains critical business rules (e.g., canceling/Invalidating the booking if payment is not received within 15 minutes, calculating the total price including taxes). It often acts as an 'Aggregate' containing a collection of booking lines (BookingItem or Ticket).