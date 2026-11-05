using System.ComponentModel.DataAnnotations;
using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

// Ticket: (1) -----> (1): Booking
// Ticket: (1) -----> (1): Concert
// Ticket: (1) -----> (1): ApplicationUser
public class Ticket : AuditableEntity
{
    public string TicketNumber { get; set; } = string.Empty; // Unique QR Code
    public string SeatNumber { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsScanned { get; set; }
    public Guid BookingId { get; set; }
    public virtual Booking? Booking { get; set; }
    public Guid ConcertId { get; set; }
    public virtual Concert? Concert { get; set; }
    public Guid CustomerId { get; set; } 
    public virtual Customer? Customer { get; set; }
    
    // Business Core Logic
    public void Scan()// à revoir!!!
    {
        if(IsScanned)
            throw new InvalidOperationException("Ticket already scanned!");
        IsScanned = true;
    }
}
//Rôle métier: rôle métier hautement stratégique qui va bien au-delà d'un simple bout de papier numérique.
//Elle est le pivot entre l'acte d'achat et l'accès physique à l'événement. Elle incarne le droit d'entrée unique pour une seule personne et pour une place précise (ou une zone comme la fosse). Contrairement au Booking qui regroupe la transaction financière globale pour plusieurs personnes, le Ticket est individuel.