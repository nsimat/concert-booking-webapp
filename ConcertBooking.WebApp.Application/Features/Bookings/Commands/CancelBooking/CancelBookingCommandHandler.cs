using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.BookingErrorsCatalog;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CancelBooking;

public sealed class CancelBookingCommandHandler(
    IBookingRepository bookingRepository,
    ICustomerRepository customerRepository,
    ICurrentUser currentUser)
    : IRequestHandler<CancelBookingCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        if (!currentUser.IsAuthenticated || !Guid.TryParse(currentUser.UserId, out var applicationUserId))
            return BookingErrors.NotAuthenticated;

        var customer = await customerRepository.GetByApplicationUserIdAsync(applicationUserId, cancellationToken);
        if (customer is null)
            return BookingErrors.CustomerNotFound;

        var bookingToCancel = await bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);
        if (bookingToCancel is null || bookingToCancel.CustomerId != customer.Id)
            return BookingErrors.BookingNotFound(request.BookingId);

        var result = bookingToCancel.Cancel();
        if (result.IsFailure)
            return result.Error;

        await bookingRepository.UpdateAsync(bookingToCancel, cancellationToken);
        return Unit.Value;
    }
}