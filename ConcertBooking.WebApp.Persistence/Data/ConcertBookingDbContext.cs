using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcertBooking.WebApp.Persistence;

public class ConcertBookingDbContext : DbContext
{
    public ConcertBookingDbContext(DbContextOptions<ConcertBookingDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Booking> ConcertBookings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConcertBookingDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.Now;
                    entry.Entity.CreatedBy = "System";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTimeOffset.Now;
                    entry.Entity.LastModifiedBy = "System";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}