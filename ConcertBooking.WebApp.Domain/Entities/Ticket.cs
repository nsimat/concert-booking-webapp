using System.ComponentModel.DataAnnotations;
using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

public class Ticket : AuditableEntity
{
    public required string TicketNumber { get; set; } // Unique QR Code
    public required string SeatNumber { get; set; } // Null si fosse / placement libre
    public decimal Price { get; set; }
    public bool IsScanned { get; set; }
    
    [Required]
    public bool IsBooked { get; set; }
    
    // Navigation
    public Guid BookingId { get; set; }
    public virtual Booking? Booking { get; set; }

    // Navigation
    public Guid ConcertId { get; set; }
    public virtual Concert? Concert { get; set; }
    
    // Business Core
    public void Scan()// à revoir!!!
    {
        if(IsScanned)
            throw new InvalidOperationException("Ticket already scanned!");
        IsScanned = true;
    }
}
//Rôle métier: rôle métier hautement stratégique qui va bien au-delà d'un simple bout de papier numérique.
//Elle est le pivot entre l'acte d'achat et l'accès physique à l'événement. Elle incarne le droit d'entrée unique pour une seule personne et pour une place précise (ou une zone comme la fosse). Contrairement au Booking qui regroupe la transaction financière globale pour plusieurs personnes, le Ticket est individuel.