using ConcertBooking.WebApp.Application.Common;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommand : IRequest<Result<Guid>>
{
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    public Guid ConcertId { get; set; }
    public Guid ApplicationUserId { get; set; }
}