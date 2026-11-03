using ConcertBooking.WebApp.Application.DTOs;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;

public class TicketDto
{
    public Guid Id { get; set; }
    public required string TicketNumber { get; set; } // Unique QR Code
    public required string SeatNumber { get; set; } // Null si fosse / placement libre
    public decimal Price { get; set; }
    public bool IsScanned { get; set; }
    
    
    public bool IsBooked { get; set; }
    
    // Navigation
    public Guid BookingId { get; set; }
    //public BookingDto? Booking { get; set; }

    // Navigation
    public Guid ConcertId { get; set; }
    //public ConcertDto? Concert { get; set; }
}