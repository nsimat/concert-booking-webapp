namespace ConcertBooking.WebApp.Application.Features.Artists.Queries.GetArtistDetails;

public class ArtistDetailsVm
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}