using AutoMapper;
using ConcertBooking.WebApp.Application.Common;
using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Messaging;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.BookingErrorsCatalog;
using ConcertBooking.WebApp.Application.Models.Mail;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Entities;
using ConcertBooking.WebApp.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;

public sealed class CreateBookingCommandHandler(
    IBookingRepository bookingRepository,
    IConcertRepository concertRepository,
    ICustomerRepository customerRepository,
    ICurrentUser currentUser,
    IEmailService emailService,
    TimeProvider timeProvider,
    ILogger<CreateBookingCommandHandler> logger)
    : IRequestHandler<CreateBookingCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        // Input validation (ConcertId not empty) has already been done by ValidationBehavior.
        
        // 1. Check if the current user is authenticated
        if (!currentUser.IsAuthenticated || !Guid.TryParse(currentUser.UserId, out var applicationUserId))
            return BookingErrors.NotAuthenticated;
        
        var customer = await customerRepository.GetByApplicationUserIdAsync(applicationUserId, cancellationToken);
        if (customer is null)
            return BookingErrors.CustomerNotFound;
        
        // 2. Check the Business rules: Ensure the concert exists and is not in the past
        var concert = await concertRepository.GetByIdAsync(request.ConcertId, cancellationToken);
        if (concert is null)
            return BookingErrors.ConcertNotFound(request.ConcertId);
        
        var now = timeProvider.GetUtcNow();
        if (concert.DateTime <= now)
            return BookingErrors.ConcertAlreadyTookPlace(concert.Id);

        // 3. Persistence: create a new booking and save it to the database
        var booking = new Booking
        {
            ConcertId = request.ConcertId,
            CustomerId = customer.Id,
            BookedAt = now,
            Status = BookingStatus.Pending
        };
        
        booking = await bookingRepository.AddAsync(booking, cancellationToken);
        
        // 4. Notification (using the principe of best effort: an email failuremust not fail the booking creation)
        await NotifyCustomerAsync(customer, booking, cancellationToken);
        
        // 5. Success: return the ID of the newly created booking
        return booking.Id;
    }
    
    private async Task NotifyCustomerAsync(Customer customer, Booking booking, CancellationToken cancellationToken)
    {
        var email = new Email
        {
            To = customer.Email,
            Subject = "Your booking has been created",
            Body = $"Your booking {booking.Id} was created on {booking.BookedAt:f}. " +
                   "It will be confirmed once payment has been received."
        };

        try
        {
            await emailService.SendEmailAsync(email, cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            logger.LogWarning(exception, "Failed to send booking creation email for booking {BookingId}", booking.Id);
        }
    }
}