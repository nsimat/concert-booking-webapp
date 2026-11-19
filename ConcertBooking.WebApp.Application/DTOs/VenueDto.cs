namespace ConcertBooking.WebApp.Application.DTOs;

public class VenueDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int SeatCapacity { get; set; }
}