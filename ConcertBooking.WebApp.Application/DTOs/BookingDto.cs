using ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.DTOs;

public class BookingDto
{
    public Guid Id { get; set; }
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    public Guid ConcertId { get; set; }
    public ConcertDto? Concert { get; set; } = null!;
    public Guid ApplicationUserId { get; set; }
    public UserDto? ApplicationUser { get; set; } = null!;
}