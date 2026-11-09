using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Domain.Errors;

/// <summary>
/// Provides factory members for domain errors related to booking operations.
/// </summary>
/// <remarks>
/// This class centralizes booking-specific error definitions, including invalid status transitions,
/// ticket validation failures, elapsed or active payment windows, and restrictions on modifying
/// bookings that already contain tickets.
/// </remarks>
public static class BookingDomainErrors
{
    /// <summary>
    /// Creates a conflict error indicating that the specified action cannot be performed while the booking has the current status.
    /// </summary>
    /// <param name="current">The current status of the booking.</param>
    /// <param name="action">The action that was attempted.</param>
    /// <returns>An error describing the invalid booking status for the attempted action.</returns>
    public static Error InvalidStatus(BookingStatus current, string action) =>
        Error.Conflict("Booking.InvalidStatus", $"Cannot {action} a booking whose status is '{current}'.");

    /// <summary>
    /// Gets a validation error indicating that a booking cannot be confirmed unless it contains at least one ticket.
    /// </summary>
    /// <returns>An error describing the validation failure.</returns>
    public static Error NoTickets =>
        Error.Validation("Booking.NoTickets", "A booking cannot be confirmed without at least one ticket.");

    /// <summary>
    /// Gets a conflict error indicating that the payment window for the booking has elapsed.
    /// </summary>
    /// <returns>An error describing the elapsed payment window.</returns>
    public static Error PaymentWindowElapsed =>
        Error.Conflict("Booking.PaymentWindowElapsed", "The payment window for this booking has elapsed.");

    /// <summary>
    /// Gets a conflict error indicating that the payment window for the booking is still open.
    /// </summary>
    /// <returns>An error describing the open payment window.</returns>
    public static Error PaymentWindowNotElapsed =>
        Error.Conflict("Booking.PaymentWindowNotElapsed", "The payment window for this booking is still open.");

    /// <summary>
    /// Gets a conflict error indicating that the concert of a booking cannot be changed if it already has tickets.
    /// </summary>
    /// <returns>An error describing the restriction.</returns>
    public static Error HasTickets =>
        Error.Conflict("Booking.HasTickets", "Cannot change the concert of a booking that already has tickets.");
}