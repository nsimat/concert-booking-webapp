using AutoMapper;
using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.BookingErrorsCatalog;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingsList;

public sealed class GetBookingsListQueryHandler(
    ICurrentUser currentUser, 
    IBookingRepository bookingRepository, 
    ICustomerRepository customerRepository,
    IMapper mapper)
    : IRequestHandler<GetBookingsListQuery, Result<List<BookingListVm>>>
{
    public async Task<Result<List<BookingListVm>>> Handle(GetBookingsListQuery request, CancellationToken cancellationToken)
    {
        // 1. Check if the current user is authenticated
        if (!currentUser.IsAuthenticated || !Guid.TryParse(currentUser.UserId, out var applicationUserId))
            return BookingErrors.NotAuthenticated;
        
        var customer = await customerRepository.GetByApplicationUserIdAsync(applicationUserId, cancellationToken);
        if (customer is null)
            return BookingErrors.CustomerNotFound;
        
        // 2. Only bookings related to the customer are retrieved
        var allBookings = await bookingRepository.GetByCustomerIdAsync(customer.Id, cancellationToken);
        
        // 3. returns the list of customer's bookings
        return mapper.Map<List<BookingListVm>>(allBookings);
        //return Result<List<BookingListVm>>.Success(mapper.Map<List<BookingListVm>>(allBookings));
    }
}