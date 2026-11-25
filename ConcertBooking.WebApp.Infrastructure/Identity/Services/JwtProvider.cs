using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Infrastructure.Identity.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ConcertBooking.WebApp.Infrastructure.Identity.Services;

internal sealed class JwtTokenService(IOptions<JwtSettings> options, TimeProvider clock) : IJwtProvider
{
    private readonly JwtSettings _settings = options.Value;

    public (string Token, DateTimeOffset ExpiresAt) CreateAccessToken(Guid userId, string email, IEnumerable<string> roles)
    {
        var expiresAt = clock.GetUtcNow().UtcDateTime.AddMinutes(_settings.ExpiryInMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.CreateVersion7().ToString())
        };
        claims.AddRange(roles.Select(role => new Claim("role", role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        return (new JsonWebTokenHandler().CreateToken(descriptor), new DateTimeOffset(expiresAt));
    }

    public string CreateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    public string Hash(string refreshToken) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
}