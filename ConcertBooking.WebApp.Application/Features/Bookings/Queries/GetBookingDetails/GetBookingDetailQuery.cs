using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;

public class GetBookingDetailQuery : IRequest<Result<BookingDetailVm>>
{
    public Guid Id { get; set; }
}