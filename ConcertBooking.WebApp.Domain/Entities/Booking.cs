using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Enums;
using ConcertBooking.WebApp.Domain.Errors;
namespace ConcertBooking.WebApp.Domain.Entities;

// Booking: (1) -----> (*): Ticket
public sealed class Booking : AuditableEntity
{
    public const decimal TaxRate = 0.06m;
    
    public static readonly TimeSpan PaymentWindow = TimeSpan.FromMinutes(15); // 15 minutes expiration time
    public Guid ConcertId { get; set; }
    public Concert Concert { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTimeOffset BookedAt { get; set; }
    public BookingStatus Status { get; set; }
    public int SeatsReserved { get; set; }
    
    public string? CancellationReason { get; set; } // Reason for cancellation, if applicable
    public string? RefundTransactionId { get; set; } // Transaction ID for the refund, if applicable
    
    public ICollection<Ticket> Tickets { get; private set; } = new HashSet<Ticket>();
    
    // Core Business Logic
    public decimal TotalPrice => Tickets.Sum(t => t.Price);

    public decimal TotalPriceWithTaxes => TotalPrice * TaxRate;

    /// <summary>
    /// Gets a value indicating whether the booking is awaiting payment or payment retry.
    /// </summary>
    /// <value>
    /// <see langword="true" /> when the booking status is pending or payment failed; otherwise, <see langword="false" />.
    /// </value>
    private bool IsWaitingPayment => Status is BookingStatus.Pending or BookingStatus.PaymentFailed;

    /// <summary>
    /// Determines whether the payment window for this booking has elapsed.
    /// </summary>
    /// <param name="currentTime">The date and time used to evaluate the payment window.</param>
    /// <returns>
    /// <c>true</c> if the booking is waiting for payment and <paramref name="currentTime" /> is greater than or equal to the booking time plus the allowed payment window; otherwise, <c>false</c>.
    /// </returns>
    private bool IsPaymentWindowElapsed(DateTimeOffset currentTime)
    {
        if (!IsWaitingPayment)
            return false;

        return currentTime >= BookedAt + PaymentWindow;
    }

    /// <summary>
    /// Adds a ticket for the specified seat to this booking when the booking is pending.
    /// </summary>
    /// <param name="seatNumber">The seat number assigned to the ticket.</param>
    /// <param name="price">The price of the ticket.</param>
    /// <param name="qrCode">The QR code or unique ticket number for the ticket.</param>
    /// <returns>
    /// A successful result if the ticket is added; otherwise, a failure result indicating that the booking status does not allow adding a ticket.
    /// </returns>
    public Result AddTicket(string seatNumber, decimal price, string qrCode = "")
    {
        if (Status != BookingStatus.Pending)
            return BookingDomainErrors.InvalidStatus(Status, "add a ticket to");
        
        if(price <= 0)
            return BookingDomainErrors.InvalidPrice;
        
        if(Tickets.Any(t => t.SeatNumber == seatNumber))
            return BookingDomainErrors.SeatAlreadyAdded(seatNumber);
        
        Tickets.Add(new Ticket
        {
            ConcertId = this.ConcertId,
            BookingId = Id,
            CustomerId = CustomerId,
            Price = price,
            SeatNumber = seatNumber,
            TicketNumber = qrCode // Generate a QRCode or unique ticket number based on booking and concert details
        });
        SeatsReserved = Tickets.Count;
        
        return Result.Success();
    }

    /// <summary>
    /// Changes the concert associated with this booking when the booking is still pending and has no tickets.
    /// </summary>
    /// <param name="concertId">The identifier of the concert to assign to this booking.</param>
    /// <param name="concertDateTime">The scheduled date and time of the concert to assign.</param>
    /// <param name="currentTime"></param>
    /// <returns>
    /// A successful result if the concert is changed; otherwise, a failure result when the booking status does not allow the change, the concert date and time is not in the future, or the booking already has tickets.
    /// </returns>
    public Result ChangeConcert(Guid concertId, DateTimeOffset concertDateTime, DateTimeOffset currentTime) // à revoir
    {
        if (Status != BookingStatus.Pending)
            return BookingDomainErrors.InvalidStatus(Status, "change the concert of");
        
        if (concertDateTime <= currentTime)
            return BookingDomainErrors.ConcertInThePast;

        if (Tickets.Count != 0)
            return BookingDomainErrors.HasTickets;
        
        ConcertId = concertId;
        return Result.Success();
    }
    
    // Pending | PaymentFailed → Confirmed
    /// <summary>
    /// Confirms the payment for this booking when it is awaiting payment, within the allowed payment window, and has at least one ticket.
    /// </summary>
    /// <param name="currentTime">The date and time used to determine whether the payment window is still valid.</param>
    /// <returns>
    /// A successful result if the booking payment is confirmed; otherwise, an error result indicating that the booking status is invalid, the payment window has elapsed, or the booking has no tickets.
    /// </returns>
    public Result ConfirmPayment(DateTimeOffset currentTime) //à revoir!!!
    {
        if (!IsWaitingPayment)
            return BookingDomainErrors.InvalidStatus(Status, "confirm the payment of");
        
        if (IsPaymentWindowElapsed(currentTime))
            return BookingDomainErrors.PaymentWindowElapsed;
            
        if (Tickets.Count == 0)
            return BookingDomainErrors.NoTickets;

        Status = BookingStatus.Confirmed;
        return Result.Success();
    }
    
    // Pending → PaymentFailed (the customer may retry within the window)
    /// <summary>
    /// Marks the booking as payment-failed when it is currently pending.
    /// </summary>
    /// <returns>
    /// A successful result if the booking status is changed to payment-failed; otherwise, a failure result indicating that the current status does not allow the booking to be marked as payment-failed.
    /// </returns>
    public Result MarkPaymentFailed()
    {
        if (Status != BookingStatus.Pending)
            return BookingDomainErrors.InvalidStatus(Status, "mark as payment-failed");

        Status = BookingStatus.PaymentFailed;
        return Result.Success();
    }
    
    // Pending | PaymentFailed → Expired, only once the window has elapsed
    /// <summary>
    /// Expires the booking when it is awaiting payment and its payment window has elapsed.
    /// </summary>
    /// <param name="currentTime">The date and time used to determine whether the payment window has elapsed.</param>
    /// <returns>
    /// A successful result if the booking is expired; otherwise, a failure result indicating that the booking status is invalid for expiration or that the payment window is still open.
    /// </returns>
    public Result Expire(DateTimeOffset currentTime) // à revoir
    {
        if (!IsWaitingPayment)
            return BookingDomainErrors.InvalidStatus(Status, "expire");
        
        if (!IsPaymentWindowElapsed(currentTime))
            return BookingDomainErrors.PaymentWindowNotElapsed;
        
        Status = BookingStatus.Expired;
        
        return Result.Success();
    }
    
    // Pending | PaymentFailed → Canceled (nothing was paid)
    /// <summary>
    /// Cancels this booking when it is still awaiting payment.
    /// </summary>
    /// <returns>
    /// A successful result if the booking status is changed to canceled; otherwise, a failure result indicating that the current booking status cannot be canceled.
    /// </returns>
    public Result Cancel()
    {
        if (!IsWaitingPayment)
            return BookingDomainErrors.InvalidStatus(Status, "cancel");

        Status = BookingStatus.Canceled;
        
        return Result.Success();
    }
    
    // Confirmed → Refunded
    /// <summary>
    /// Refunds this booking when it is in the confirmed state.
    /// </summary>
    /// <returns>
    /// A successful result if the booking was confirmed and its status was changed to refunded; otherwise, a failure result indicating that the current status does not allow refunding.
    /// </returns>
    public Result Refund()
    {
        if (Status != BookingStatus.Confirmed)
            return BookingDomainErrors.InvalidStatus(Status, "refund");
        
        Status = BookingStatus.Refunded;
        return Result.Success();
    } //public bool IsPaid => Status == BookingStatus.Paid; // on paie le booking qui génère un ticket
    
    // Only bookings that are no longer active and were never paid can be deleted
    /// <summary>
    /// Ensures that the booking is in a status that allows deletion.
    /// </summary>
    /// <returns>
    /// A successful result if the booking status is <see cref="BookingStatus.Canceled" /> or <see cref="BookingStatus.Expired" />; otherwise, a failure result indicating that the current status does not allow deletion.
    /// </returns>
    public Result EnsureCanBeDeleted() =>
        Status is BookingStatus.Canceled or BookingStatus.Expired
            ? Result.Success()
            : BookingDomainErrors.InvalidStatus(Status, "delete");
}
//Rôle métier: Contains critical business rules (e.g., canceling/Invalidating the booking if payment is not received within 15 minutes, calculating the total price including taxes). It often acts as an 'Aggregate' containing a collection of booking lines (BookingItem or Ticket).