namespace ConcertBooking.WebApp.Domain.Common;

/// <summary>
/// Represents the base class for entities that track auditing information such as
/// creation and modification details. It is intended to be inherited by other entity
/// classes where audit tracking is required.
/// </summary>
public abstract class AuditableEntity : BaseEntity   
{
    /// <summary>
    /// Gets or sets the unique identifier of the user or entity responsible for creating the record.
    /// This property is typically populated during the creation of the entity.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time at which the record was created.
    /// This property is typically set during the initialization of the entity.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user or entity responsible for the most recent modification of the record.
    /// This property is typically updated whenever the entity is modified.
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the record was last modified.
    /// This property is updated whenever any modification is made to the entity.
    /// </summary>
    public DateTimeOffset? LastModifiedAt { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the entity is marked as deleted (soft delete).
    /// When set to <c>true</c>, the entity is considered deleted and should not be included in active queries, but it remains in the database for historical or auditing purposes.
    /// </summary>
    public bool IsDeleted { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the date and time when the entity was marked as deleted (soft delete).
    /// This property is typically set when the entity is marked as deleted, allowing for tracking of when the deletion occurred.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the user or entity responsible for marking the entity as deleted (soft delete).
    /// This property is typically populated when the entity is marked as deleted, allowing for tracking of who performed the deletion action.
    /// </summary>
    public string DeletedBy { get; set; } = string.Empty;
}