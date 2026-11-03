using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.DTOs;

public class ConcertDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; } = string.Empty;// Name
    public string? Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public ConcertStatus Status { get; set; }
    public DateTime DateTime { get; set; }
    
    public Guid VenueId { get; set; }
    public VenueDto? Venue { get; set; }
    
    public Guid ArtistId { get; set; } 
    public ArtistDto? Artist { get; set; }
}