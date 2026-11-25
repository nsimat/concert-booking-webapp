using ConcertBooking.WebApp.Infrastructure.Identity.Entities;

namespace ConcertBooking.WebApp.Infrastructure.Identity.Services;

internal interface ITokenService
{
    (string Token, DateTimeOffset ExpiresAt) CreateAccessToken(ApplicationUser user, IEnumerable<string> roles);
    string CreateRefreshToken();
    string Hash(string refreshToken);
}