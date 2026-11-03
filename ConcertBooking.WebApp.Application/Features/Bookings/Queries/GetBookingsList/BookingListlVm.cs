using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingsList;

public class BookingListlVm
{
    public Guid BookingId { get; set; }
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
}