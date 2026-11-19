using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.BookingErrorsCatalog;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.DeleteBooking;

public class DeleteBookingCommandHandler(
    IBookingRepository bookingRepository,
    ICustomerRepository customerRepository,
    ICurrentUser currentUser)
    : IRequestHandler<DeleteBookingCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        // 1. check if the current user is authenticated and authorized to delete the booking i.e. the current user has to be the owner of the booking or an admin
        if (!currentUser.IsAuthenticated || !Guid.TryParse(currentUser.UserId, out var applicationUserId))
            return BookingErrors.NotAuthenticated;
        
        var customer = await customerRepository.GetByApplicationUserIdAsync(applicationUserId, cancellationToken);
        if (customer is null)
            return BookingErrors.CustomerNotFound;
        
        // 2. Check the Business rules: Check if the booking exists and belongs to the authenticated customer
        var bookingToDelete = await bookingRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bookingToDelete is null || bookingToDelete.CustomerId != customer.Id)
            return BookingErrors.BookingNotFound(request.Id); 
        
        // 3. Domain rule: only finished/unpaid bookings can be deleted
        var canDelete = bookingToDelete.EnsureCanBeDeleted();
        if (canDelete.IsFailure)
            return canDelete.Error;
        
        // 4. Persistence: delete the booking from the database
        await bookingRepository.DeleteAsync(bookingToDelete, cancellationToken);
        
        // 5. Success: return a successful result
        return Unit.Value;
    }
}