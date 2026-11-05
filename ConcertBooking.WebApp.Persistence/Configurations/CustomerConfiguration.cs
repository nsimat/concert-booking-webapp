using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer> // Est-il nécessaire de créer une configuration pour Customer si on utilise IdentityUser? --- IGNORE ---
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        entity.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);
        entity.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(50);
        entity.Property(c => c.PhoneNumber)
            .HasMaxLength(20);
        
        entity.HasMany(c => c.Bookings)
            .WithOne(b => b.Customer)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity.HasMany(c => c.Tickets)
            .WithOne(t => t.Customer)
            .HasForeignKey(t => t.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // insert seed data with HasData()
    }
}