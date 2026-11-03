using AutoMapper;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.DTOs;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Entities;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;

public class GetBookingDetailQueryHandler : IRequestHandler<GetBookingDetailQuery, Result<BookingDetailVm>>
{
    private readonly IAsyncRepository<Booking> _bookingRepository;
    private readonly IAsyncRepository<Concert> _concertRepository;
    private readonly IAsyncRepository<ApplicationUser> _userRepository;
    private readonly IMapper _mapper;

    public GetBookingDetailQueryHandler(
        IAsyncRepository<Booking> bookingRepository, 
        IAsyncRepository<Concert> concertRepository,
        IAsyncRepository<ApplicationUser> userRepository,
        IMapper mapper)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _concertRepository = concertRepository ?? throw new ArgumentNullException(nameof(concertRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<Result<BookingDetailVm>> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(request.Id);
        var bookingDetailDto = _mapper.Map<BookingDetailVm>(booking);

        if (booking is null)
            return Result<BookingDetailVm>.Failure(Error.NotFound("Booking.NotFound",$"Booking with ID '{request.Id} not found!'"));

        var concert = await _concertRepository.GetByIdAsync(booking.ConcertId);
        var user = await _userRepository.GetByIdAsync(booking.ApplicationUserId);

        //bookingDetailDto.Concert = _mapper.Map<ConcertDto>(concert);
        //bookingDetailDto.ApplicationUser = _mapper.Map<UserDto>(user);

        return Result<BookingDetailVm>.Success(bookingDetailDto);// à revoir
    }
}