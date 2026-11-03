using ConcertBooking.WebApp.Application.Contracts.Persistence;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.DeleteBooking;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
{
    private readonly IBookingRepository _bookingRepository;

    public DeleteBookingCommandHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));

    }
    
    public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingToDelete = await _bookingRepository.GetByIdAsync(request.Id);

        if (bookingToDelete != null) await _bookingRepository.DeleteAsync(bookingToDelete);
        
        return Unit.Value;
    }
}