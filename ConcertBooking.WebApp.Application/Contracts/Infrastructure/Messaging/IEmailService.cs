using ConcertBooking.WebApp.Application.Models.Mail;

namespace ConcertBooking.WebApp.Application.Contracts.Infrastructure.Messaging;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Email email, CancellationToken cancellationToken = default);
}