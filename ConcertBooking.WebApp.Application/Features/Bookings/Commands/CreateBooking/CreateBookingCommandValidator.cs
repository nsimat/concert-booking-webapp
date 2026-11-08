using FluentValidation;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(b => b.ConcertId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required!");
    }
}