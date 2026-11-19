using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Application.Features.Customers.CustomerErrorsCatalog;

public static class CustomerErrors
{
    public static Error CustomerNotCreated =>
        Error.NotFound("Booking.CustomerNotCreated", "No customer profile was created for the current user.");
}