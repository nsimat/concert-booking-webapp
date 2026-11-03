using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.DTOs;

public class ArtistDto
{
    public Guid Id { get; set; }
    public required string StageName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public MusicalStyle MusicalStyle { get; set; }
    public string? ImageUrl { get; set; }
}