using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> entity)
    {
        entity.HasKey(b => b.Id);
        entity.Property(b => b.BookedAt)
            .IsRequired();
        entity.Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
        
        entity.HasOne(b => b.Concert)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.ConcertId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(b => b.Customer)
            .WithMany(a => a.Bookings)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(b => b.Tickets)
            .WithOne(t => t.Booking)
            .HasForeignKey(t => t.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // insert seed date with HasData()
    }
}