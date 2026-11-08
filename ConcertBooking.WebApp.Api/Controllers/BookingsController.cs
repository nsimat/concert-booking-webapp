using ConcertBooking.WebApp.Api.Contracts.Bookings;
using ConcertBooking.WebApp.Api.Utilities;
using ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;
using ConcertBooking.WebApp.Application.Features.Bookings.Commands.DeleteBooking;
using ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;
using ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.WebApp.Api.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookingsController(IMediator mediator) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllBookings(CancellationToken cancellationToken)
    {
        var getBookingsListQuery = new GetBookingsListQuery();
        
        var result = await mediator.Send(getBookingsListQuery, cancellationToken);

        return this.FromResult(result, Ok);
    }
    
    [HttpGet("{bookingId:guid}")]
    public async Task<IActionResult> GetBookingById(
        Guid bookingId, 
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBookingDetailQuery { Id = bookingId }, cancellationToken);
        return this.FromResult(result, Ok);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBooking(
        [FromBody] CreateBookingRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new CreateBookingCommand()
        {
            ConcertId = request.ConcertId,
            //CustomerId = Guid.Empty // _currentUserService.UserId
        };// BookedAt and Status are set in the command handler, not here
        
        var result = await mediator.Send(command, cancellationToken);
        
        return this.FromResult(result, bookingId => CreatedAtAction(nameof(GetBookingById), new { bookingId }, new { bookingId }));
    }
    
    [HttpPut("{bookingId:guid}")]
    public async Task<IActionResult> UpdateBooking(
        Guid bookingId, 
        [FromBody] UpdateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(bookingId);
        var result = await mediator.Send(command, cancellationToken);

        return this.FromResult(result, _ => NoContent());
    }
    
    [HttpDelete("{bookingId:guid}")]
    public async Task<IActionResult> DeleteBooking(Guid bookingId, CancellationToken cancellationToken){
        var result = await mediator.Send(new DeleteBookingCommand { Id = bookingId }, cancellationToken);
        return this.FromResult(result, _ => NoContent());
    }
}