using System.ComponentModel.DataAnnotations;
using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

// A venue (1) ------> * (Concert)
public class Venue : AuditableEntity
{
    public required string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    
    [Required]
    public int SeatCapacity { get; set; }
    public virtual ICollection<Concert> Concerts { get; set; } = new HashSet<Concert>();
}
// Rôle métier: It dictates the maximum ticket cap available for a given concert to prevent overbooking.