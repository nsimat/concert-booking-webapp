using AutoMapper;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Entities;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQueryHandler : IRequestHandler<GetBookingsListQuery, Result<List<BookingListlVm>>>
{
    private readonly IAsyncRepository<Booking> _bookingRepository;
    private readonly IMapper _mapper;

    public GetBookingsListQueryHandler(IAsyncRepository<Booking> bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<Result<List<BookingListlVm>>> Handle(GetBookingsListQuery request, CancellationToken cancellationToken)
    {
        var allBookings = (await _bookingRepository.GetAllAsync()).OrderBy(x => x.BookingDate);
        
        return Result<List<BookingListlVm>>.Success(_mapper.Map<List<BookingListlVm>>(allBookings));
    }
}