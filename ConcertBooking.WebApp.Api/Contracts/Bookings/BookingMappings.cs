using ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;

namespace ConcertBooking.WebApp.Api.Contracts.Bookings;

public static class BookingMappings
{
    public static UpdateBookingCommand ToCommand(this UpdateBookingRequest request, Guid id) => new()
    {
        BookingId = id,
        ConcertId = request.ConcertId
    };
}