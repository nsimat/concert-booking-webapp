using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer> 
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.Property(c => c.Id).ValueGeneratedNever(); // the ID comes from ApplicationUser entity
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
            .HasForeignKey(b => b.CustomerId);
        
        entity.HasMany(c => c.Tickets)
            .WithOne(t => t.Customer)
            .HasForeignKey(t => t.CustomerId);
        
        // insert seed data with HasData()
    }
}