using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Application.Contracts.Persistence;

public interface IBookingRepository : IAsyncRepository<Booking>
{
    /// <summary>
    /// Loads a booking with its tickets (needed by ConfirmPayment/ChangeConcert).
    /// </summary>
    Task<Booking?> GetByIdWithTicketsAsync(Guid bookingId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Bookings still awaiting payment whose window ended before <paramref name="threshold"/>.
    /// </summary>
    Task<IReadOnlyList<Booking>> GetAwaitingPaymentBookedBeforeAsync(DateTimeOffset threshold, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    
    Task<Booking?> GetByIdWithDetailsAsync(Guid bookingId, CancellationToken cancellationToken = default);
}