using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CancelBooking;

public sealed record CancelBookingCommand(Guid BookingId) : IRequest<Result<Unit>>;