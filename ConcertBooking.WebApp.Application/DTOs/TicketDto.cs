namespace ConcertBooking.WebApp.Application.DTOs;

public class TicketDto
{
    public Guid Id { get; set; }
    public required string TicketNumber { get; set; } // Unique QR Code
    public required string SeatNumber { get; set; } 
    public decimal Price { get; set; }
    public bool IsScanned { get; set; }
    public bool IsBooked { get; set; }
    
    // Navigation
    public Guid BookingId { get; set; }
    
    // Navigation
    public Guid ConcertId { get; set; }
}