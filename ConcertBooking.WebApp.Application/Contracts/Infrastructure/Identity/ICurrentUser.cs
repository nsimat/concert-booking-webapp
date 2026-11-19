namespace ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;

/// <summary>
/// Represents the currently authenticated user within the application,
/// providing user-related information such as ID, email, and authentication state.
/// </summary>
public interface ICurrentUser
{
    public string? UserId { get; }
    public string? Email { get; }
    public bool IsAuthenticated { get; }
    bool IsInRole(string role); // à revoir
}

public static class Roles
{
    public const string Customer = "Customer";
    public const string Admin = "Admin";
    public const string Anonymous = "Anonymous";
    public const string Controller = "Controller";
}