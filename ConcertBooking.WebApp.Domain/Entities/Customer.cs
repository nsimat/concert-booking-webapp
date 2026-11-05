using System.Collections;
using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

// One-to-many relationship with Booking: A customer (1) -----> (*) Bookings
public class Customer : AuditableEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public Guid ApplicationUserId { get; set; } // lien vers la sécurité de l'application (IdentityUser)
    public virtual ApplicationUser? ApplicationUser { get; set; } // Comment connaître l'ApplicationUser associé au Customer alors que ApplicationUser, qui est dans le projet Infrastructure, n'a pas de référence vers Customer ? (réflexion sur la conception de l'architecture) --> ApplicationUser se trouve dans le projet ConcertBooking.WebApp.Persistence, donc il n'est pas directement accessible depuis le projet ConcertBooking.WebApp.Domain. Il faudrait envisager de créer une interface ou un service pour gérer cette relation, ou bien repenser la structure des projets pour permettre un accès plus direct aux entités nécessaires.
    
    // propriétés métier 
    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
}