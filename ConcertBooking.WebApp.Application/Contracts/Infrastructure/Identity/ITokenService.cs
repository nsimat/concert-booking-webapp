using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;

/// <summary>
/// Provides functionality for generating and refreshing authentication tokens.
/// </summary>
public interface ITokenService
{
    Task<TokenResponse> GenerateTokenAsync(Customer user, CancellationToken cancellationToken = default);
    Task<TokenResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents the response returned after generating or refreshing an authentication token.
/// </summary>
/// <param name="AccessToken">The access token used for authenticating subsequent requests.</param>
/// <param name="RefreshToken">The refresh token used to obtain a new access token when the current one expires.</param>
/// <param name="ExpiresAt">The expiration date and time of the access token.</param>
public sealed record TokenResponse(string AccessToken, string RefreshToken, DateTimeOffset ExpiresAt);