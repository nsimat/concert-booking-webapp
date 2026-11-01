namespace ConcertBooking.WebApp.Domain.Enums;

public enum BookingStatus
{
    Pending,       // The initial status when the booking is created
    PaymentFailed, // Payment processing failed
    Confirmed,     // Paid, Payment successfully processed
    Expired,       // Booking slot time expired 
    Refunded       // Booking is refunded
}