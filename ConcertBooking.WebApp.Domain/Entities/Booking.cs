using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;

namespace ConcertBooking.WebApp.Domain.Entities;

// Booking (1) -----> (*) Ticket
public class Booking : AuditableEntity
{
    [Required]
    public DateTime BookingDate { get; set; }

    public BookingStatus Status { get; set; }
    
    // Navigation
    [Required]
    [ForeignKey("ConcertId")]
    public Guid ConcertId { get; set; }
    public virtual Concert Concert { get; set; } = null!;
    
    // Navigation
    [Required]
    [ForeignKey("ApplicationUserId")]
    public Guid ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    
    public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    
    // Core Business
    public decimal TotalPrice => Tickets.Sum(t => t.Price);

    public decimal TotalPriceWithTaxes => TotalPrice * 1.2m;

    private Booking() { } // For Entity Framework

    public Booking(Guid customerId, Guid concertId)
    {
        ApplicationUserId = customerId;
        ConcertId = concertId;
        BookingDate = DateTime.UtcNow;
        Status = BookingStatus.Pending;
    }
    
    public void AddTicket(string seatNumber, decimal price)
    {
        if(Status == BookingStatus.Pending)
            throw new InvalidOperationException("Cannot add ticket to a booking that is not pending.");
        
        string uniqueTicketNumber = 
            $"TCK-{ApplicationUserId.ToString()[..8].ToUpper()}-{ConcertId.ToString()[..8].ToUpper()}-{seatNumber}";
        var ticket = new Ticket()
        {
            ConcertId = this.ConcertId,
            Price = price,
            BookingId = this.Id,
            SeatNumber = seatNumber,
            TicketNumber = uniqueTicketNumber
        };
        Tickets.Add(ticket);
    }
    
    public void ConfirmPayment()
    {
        if (Status == BookingStatus.Expired)
            throw new InvalidOperationException("The booking time has expired. Impossible to pay it.");
            
        if (!Tickets.Any())
            throw new InvalidOperationException("Impossible to confirm the booking without a ticket.");

        Status = BookingStatus.Confirmed;
    }
    
    public void Expire()// à revoir
    {
        if (Status == BookingStatus.Pending)
        {
            Status = BookingStatus.Expired;
        }
    }
    
    public void Cancel()//à revoir
    {
        if(Status != BookingStatus.Pending)
            throw new InvalidOperationException("Cannot cancel a booking that is not pending.");

        Status = BookingStatus.Refunded;
    }
    //public bool IsPaid => Status == BookingStatus.Paid; // on paie le booking qui genère un ticket
}
//Rôle métier: Contains critical business rules (e.g., canceling/Invalidating the booking if payment is not received within 15 minutes, calculating the total price including taxes). It often acts as an 'Aggregate' containing a collection of booking lines (BookingItem or Ticket).