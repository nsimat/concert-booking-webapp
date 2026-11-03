using ConcertBooking.WebApp.Application.Contracts.Persistence;
using FluentValidation;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;

    public CreateBookingCommandValidator(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        
        RuleFor(b => b.BookingDate)
            .NotEmpty().WithMessage("{PropertyName} is required!")
            .NotNull()
            .LessThan(DateTime.Now).WithMessage("{PropertyName} must be today or a future date");
    }

}