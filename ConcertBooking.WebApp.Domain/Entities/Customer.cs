using ConcertBooking.WebApp.Domain.Common;

namespace ConcertBooking.WebApp.Domain.Entities;

// One-to-many relationship with Booking: A customer (1) -----> (*) Bookings
public sealed class Customer : AuditableEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; } = string.Empty;

    private Customer() { } // EF Core knows how to deal with

    public Customer(Guid applicationUserId, string firstName, string lastName, string email, string? phoneNumber)
    {
        Id = applicationUserId; // the same Guid ID than the ApplicationUser entity  
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    
    // propriétés métier 
    public ICollection<Booking> Bookings { get; private set; } = new HashSet<Booking>();
    public ICollection<Ticket> Tickets { get; private set; } = new HashSet<Ticket>();
}