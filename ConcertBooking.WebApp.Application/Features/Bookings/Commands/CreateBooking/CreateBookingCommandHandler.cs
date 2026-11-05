using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ConcertBooking.WebApp.Application.Common;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Entities;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<Guid>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public CreateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Result<Guid>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateBookingCommandValidator(_bookingRepository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);
        if (validatorResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validatorResult);

        var booking = _mapper.Map<Booking>(request);
        booking = await _bookingRepository.AddAsync(booking);
        
        return Result<Guid>.Success(booking.Id);
    }
}