using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.DeleteBooking;

public class DeleteBookingCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}