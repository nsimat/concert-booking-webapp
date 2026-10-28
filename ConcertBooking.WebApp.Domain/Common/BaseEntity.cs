namespace ConcertBooking.WebApp.Domain.Common;

/// <summary>
/// Represents the base class for all entities within the domain.
/// Provides a unique identifier for each entity instance.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; init; }
}