using System.ComponentModel.DataAnnotations;
using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

// An artist: (1) -----> (*)(Concert)
public class Artist: AuditableEntity
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Bio { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;

    public virtual ICollection<Concert> Concerts { get; set; } = new HashSet<Concert>();
}