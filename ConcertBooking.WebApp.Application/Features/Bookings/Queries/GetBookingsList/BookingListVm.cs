using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingsList;

public class BookingListVm
{
    public Guid Id { get; set; }
    public DateTimeOffset BookedAt { get; set; }
    public BookingStatus Status { get; set; }
}