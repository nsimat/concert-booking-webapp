using System.ComponentModel.DataAnnotations;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Domain.Entities;

// One artist: (1) -----> (*): Concerts
public class Artist: AuditableEntity
{
    public required string StageName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public MusicalStyle MusicalStyle { get; set; }
    public string? ImageUrl { get; set; }

    public virtual ICollection<Concert> Concerts { get; set; } = new HashSet<Concert>();// or =[] to avoid null.
}
//Rôle métier: Enables categorization, user search, and schedule management.