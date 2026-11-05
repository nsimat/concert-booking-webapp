using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Messaging;
using ConcertBooking.WebApp.Application.Models.Mail;
using Microsoft.Extensions.Logging;

namespace ConcertBooking.WebApp.Infrastructure.Mail;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<bool> SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Email notification: recipient {Recipient}; subject {Subject}; body {Body}",
            email.To,
            email.Subject,
            email.Body);

        return await Task.FromResult(true);
    }
}