using ConcertBooking.WebApp.Application.DTOs;
using ConcertBooking.WebApp.Domain.Entities;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;

public class BookingDetailVm
{
    public Guid Id { get; set; }
    public DateTimeOffset BookedAt { get; set; }
    public BookingStatus Status { get; set; }
    public Guid ConcertId { get; set; }
    public ConcertDto? Concert { get; set; }
    public Guid CustomerId { get; set; }
    public UserDto? Customer { get; set; }
}