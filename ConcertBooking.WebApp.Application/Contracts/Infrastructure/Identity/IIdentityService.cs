using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;

public interface IIdentityService
{
    Task<Result<Guid>> RegisterAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Result<TokenResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Result<TokenResponse>> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default);
} 