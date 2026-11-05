using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ConcertBooking.WebApp.Application.Common;
using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Messaging;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Models.Mail;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Entities;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<Guid>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CreateBookingCommandHandler(
        IBookingRepository bookingRepository, 
        IMapper mapper,
        IEmailService emailService)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }
    
    public async Task<Result<Guid>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateBookingCommandValidator(_bookingRepository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (validatorResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validatorResult);

        var booking = _mapper.Map<Booking>(request);
        booking = await _bookingRepository.AddAsync(booking, cancellationToken);

        var email = new Email()
        {
            To = "customer@own.mail", // The email of the connected customer
            Subject = "A new booking was created.",
            Body = $"A new booking was created: {request}"
        };

        try
        {
            await _emailService.SendEmailAsync(email, cancellationToken);

        }
        catch (Exception e)
        {
            // A failed notification should not stop the API from doing its work, so we log the exception and move on.
            Console.WriteLine(e);
        }
        
        return Result<Guid>.Success(booking.Id);
    }
}