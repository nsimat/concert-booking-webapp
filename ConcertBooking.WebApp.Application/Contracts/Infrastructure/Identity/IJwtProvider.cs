using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;

/// <summary>
/// Provides functionality for generating and refreshing authentication tokens.
/// </summary>
public interface ITokenService1 // Sa place est-elle ici ou dans le projet 'persistence' ?
{
    Task<TokenResponse> GenerateTokenAsync(Customer user, IEnumerable<string> roles, CancellationToken cancellationToken = default);
    Task<TokenResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
}