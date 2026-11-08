using ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;
using FluentValidation;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
        RuleFor(b => b.ConcertId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required!");
    }
}