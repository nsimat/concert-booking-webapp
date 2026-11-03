using ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingsList;

public class GetBookingsListQuery : IRequest<Result<List<BookingListlVm>>>
{
}