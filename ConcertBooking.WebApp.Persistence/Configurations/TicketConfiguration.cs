using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> entity)
    {
        entity.HasKey(t => t.Id);
        entity.Property(t => t.TicketNumber)
            .IsRequired()
            .HasMaxLength(50);
        entity.Property(t => t.SeatNumber)
            .IsRequired()
            .HasMaxLength(10);
        entity.Property(t => t.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");//à revoir!!!
        entity.Property(t => t.IsScanned)
            .IsRequired()
            .HasDefaultValue(false);

        entity.HasOne(t => t.Booking)
            .WithMany(b => b.Tickets)
            .HasForeignKey(t => t.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(t => t.Concert)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.ConcertId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasOne(t => t.Customer)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // insert seed data with HasData()
    }
}