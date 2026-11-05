using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Persistence.Repositories;

public class VenueRepository : BaseRepository<Venue>, IVenueRepository
{
    public VenueRepository(ConcertBookingDbContext dbContext) : base(dbContext)
    {
    }
}