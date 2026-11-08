using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommand : IRequest<Result<Unit>>
{
    public Guid BookingId { get; init; }
    public Guid ConcertId { get; init; }
}