using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

// Booking (1) -----> (*) Ticket
public class Booking : AuditableEntity
{
    [Key]
    public Guid BookingId { get; set; }
    
    [Required]
    public DateTime BookingDate { get; set; }
    
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