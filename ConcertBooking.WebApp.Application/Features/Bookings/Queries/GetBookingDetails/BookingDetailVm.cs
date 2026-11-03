using ConcertBooking.WebApp.Application.DTOs;
using ConcertBooking.WebApp.Domain.Entities;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;

public class BookingDetailVm
{
    public Guid BookingId { get; set; }
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    public Guid ConcertId { get; set; }
    public ConcertDto? Concert { get; set; } = null!;
    public Guid ApplicationUserId { get; set; }
    public UserDto? ApplicationUser { get; set; } = null!;
}