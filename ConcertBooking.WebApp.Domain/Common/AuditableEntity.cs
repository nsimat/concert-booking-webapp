namespace ConcertBooking.WebApp.Domain.Common;

/// <summary>
/// Represents the base class for entities that track auditing information such as
/// creation and modification details. It is intended to be inherited by other entity
/// classes where audit tracking is required.
/// </summary>
public class AuditableEntity
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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user or entity responsible for the most recent modification of the record.
    /// This property is typically updated whenever the entity is modified.
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the record was last modified.
    /// This property is updated whenever any modification is made to the entity.
    /// </summary>
    public DateTime? LastModifiedAt { get; set; }    
}