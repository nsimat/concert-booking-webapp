using AutoMapper;
using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.BookingErrorsCatalog;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;

public class GetBookingDetailQueryHandler(
    IBookingRepository bookingRepository,
    ICustomerRepository customerRepository,
    ICurrentUser currentUser,
    IMapper mapper)
    : IRequestHandler<GetBookingDetailQuery, Result<BookingDetailVm>>
{
    public async Task<Result<BookingDetailVm>> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
    {
        // 1. Check if the current user is authenticated
        if (!currentUser.IsAuthenticated || !Guid.TryParse(currentUser.UserId, out var applicationUserId))
            return BookingErrors.NotAuthenticated;
        
        // 2. Check if the authenticated user is authorized 
        var customer = await customerRepository.GetByApplicationUserIdAsync(applicationUserId, cancellationToken);
        if (customer is null)
            return BookingErrors.CustomerNotFound;
        
        // 3. Retrieve the requested booking
        var booking = await bookingRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
        if (booking is null || booking.CustomerId != customer.Id)
            return BookingErrors.BookingNotFound(request.Id);
        
        // 4. Map the booking details to the ViewModel and return the result
        var bookingDetailDto = mapper.Map<BookingDetailVm>(booking); 
        return bookingDetailDto; // Concert -> ConcertDto, Customer -> UserDto mapped via the profile
        //return Result<BookingDetailVm>.Success(bookingDetailDto);// à revoir
    }
}