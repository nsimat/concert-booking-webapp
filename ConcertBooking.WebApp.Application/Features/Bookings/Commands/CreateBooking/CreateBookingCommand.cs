using ConcertBooking.WebApp.Application.Common;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;

public sealed class CreateBookingCommand : IRequest<Result<Guid>>
{
    public Guid ConcertId { get; init; }
    //public Guid CustomerId { get; set; }
}