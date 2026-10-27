using System.ComponentModel.DataAnnotations;
using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

public class Ticket : AuditableEntity
{
    [Key]
    public Guid TicketId { get; set; }
    public required int SeatNumber { get; set; }
    
    [Required]
    public bool IsBooked { get; set; }
    
    // Navigation
    public Guid? BookingId { get; set; }
    public virtual Booking Booking { get; set; }
}