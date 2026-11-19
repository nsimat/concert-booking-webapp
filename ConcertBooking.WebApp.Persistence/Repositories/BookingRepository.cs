using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Entities;
using ConcertBooking.WebApp.Domain.Enums;
using ConcertBooking.WebApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ConcertBooking.WebApp.Persistence.Repositories;

public class BookingRepository(ConcertBookingDbContext dbContext)
    : BaseRepository<Booking>(dbContext), IBookingRepository
{
    public async Task<Booking?> GetByIdWithTicketsAsync(Guid bookingId, CancellationToken cancellationToken = default) => 
            await DbContext.Bookings
                .Include(b => b.Tickets)
                .FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);
    
    public async Task<IReadOnlyList<Booking>> GetAwaitingPaymentBookedBeforeAsync(DateTimeOffset threshold, CancellationToken cancellationToken = default) => 
            await DbContext.Bookings
                .Where(b => (b.Status == BookingStatus.Pending || b.Status == BookingStatus.PaymentFailed) &&
                        b.BookedAt.AddMinutes(30) < threshold)
                .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default) => 
            await DbContext.Bookings
                .Where(b => b.CustomerId == customerId)
                .OrderBy(b => b.BookedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

    public async Task<Booking?> GetByIdWithDetailsAsync(
        Guid bookingId, 
        CancellationToken cancellationToken = default) => 
            await DbContext.Bookings
                .Include(b => b.Concert)
                .Include(b => b.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);
}