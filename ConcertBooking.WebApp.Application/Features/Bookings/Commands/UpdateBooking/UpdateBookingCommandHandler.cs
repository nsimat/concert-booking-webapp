using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.BookingErrorsCatalog;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommandHandler(
    IBookingRepository bookingRepository,
    ICustomerRepository customerRepository,
    IConcertRepository concertRepository,
    ICurrentUser currentUser,
    TimeProvider timeProvider)
    : IRequestHandler<UpdateBookingCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        // Input validation (ConcertId not empty) has already been done by ValidationBehavior.
        
        // 1. Check if the current user is authenticated
        if (!currentUser.IsAuthenticated || !Guid.TryParse(currentUser.UserId, out var applicationUserId))
            return BookingErrors.NotAuthenticated;
        
        var customer = await customerRepository.GetByApplicationUserIdAsync(applicationUserId, cancellationToken);
        if (customer is null)
            return BookingErrors.CustomerNotFound;
        
        // 2. Check the Business rules: Check if the booking exists and belongs to the authenticated customer
        var bookingToUpdate = await bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);
        if (bookingToUpdate is null)
                return BookingErrors.BookingNotFound(request.BookingId);
        
        if (bookingToUpdate.CustomerId != customer.Id)
            return BookingErrors.BookingNotFound(request.BookingId);
        
        // 3. Check the Business rules: Ensure the concert exists and is not in the past
        var concert = await concertRepository.GetByIdAsync(request.ConcertId, cancellationToken);
        if (concert is null)
            return BookingErrors.ConcertNotFound(request.ConcertId);
        
        var now = timeProvider.GetUtcNow();
        if (concert.DateTime <= now)
            return BookingErrors.ConcertAlreadyTookPlace(concert.Id);
        
        // 4. Update the booking and save changes to the database
        bookingToUpdate.ConcertId = request.ConcertId;
        
        await bookingRepository.UpdateAsync(bookingToUpdate, cancellationToken);

        // 5. Success: return a successful result
        return Unit.Value;
    }
}