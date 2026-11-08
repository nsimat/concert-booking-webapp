using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Application.Features.Bookings.BookingErrorsCatalog;

public static class BookingErrors
{
    public static Error BookingNotFound(Guid bookingId) =>
        Error.NotFound("Booking.NotFound", $"Booking with ID '{bookingId}' was not found.");

    public static Error NotAuthenticated =>
        Error.Unauthorized("Booking.NotAuthenticated", "You must be signed in to book a concert.");

    public static Error CustomerNotFound =>
        Error.NotFound("Booking.CustomerNotFound", "No customer profile is associated with the current user.");

    public static Error ConcertNotFound(Guid concertId) =>
        Error.NotFound("Booking.ConcertNotFound", $"Concert with ID '{concertId}' was not found.");

    public static Error ConcertAlreadyTookPlace(Guid concertId) =>
        Error.Conflict("Booking.ConcertAlreadyTookPlace", $"Concert with ID '{concertId}' has already taken place.");
}