using ConcertBooking.WebApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ConcertBooking.WebApp.Persistence.Identity;

public class ApplicationUser : IdentityUser<Guid> 
{
    public string UserName { get; set; } = string.Empty; // Déjà dans IdentityUser
    public string PasswordHash { get; set; } = string.Empty; // Déjà dans IdentityUser
    public ICollection<Role> Roles { get; set; } = new HashSet<Role>(); // à revoir, car des rôles sont déjà définis aussi dans le projet Application
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}
// Rôle métier: Identify the customer and his rights on the show tickets.