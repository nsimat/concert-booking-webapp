using ConcertBooking.WebApp.Domain.Enums;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    public Guid ConcertId { get; set; }
    public Guid ApplicationUserId { get; set; }
}