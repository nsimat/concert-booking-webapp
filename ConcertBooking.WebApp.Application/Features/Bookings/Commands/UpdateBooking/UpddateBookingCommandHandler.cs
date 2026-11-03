using AutoMapper;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Entities;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;

public class UpddateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Unit>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public UpddateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingToUpdate = await _bookingRepository.GetByIdAsync(request.Id);
        _mapper.Map(request, bookingToUpdate, typeof(UpdateBookingCommand), typeof(Booking));

        if (bookingToUpdate != null) await _bookingRepository.UpdateAsync(bookingToUpdate);

        return Unit.Value;
    }
}