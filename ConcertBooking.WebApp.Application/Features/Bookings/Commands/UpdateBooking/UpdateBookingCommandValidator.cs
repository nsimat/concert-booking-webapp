using ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;
using FluentValidation;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
        RuleFor(b => b.BookingId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required!");
        RuleFor(b => b.ConcertId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required!");
    }
}