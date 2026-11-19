using ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.DTOs;

public class BookingDto
{
    public Guid Id { get; set; }
    public DateTimeOffset BookedAt { get; set; }
    public BookingStatus Status { get; set; }
    public Guid ConcertId { get; set; }
    public Guid CustomerId { get; set; }
}