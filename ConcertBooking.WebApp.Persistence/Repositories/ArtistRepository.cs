using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Persistence.Repositories;

public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
    public ArtistRepository(ConcertBookingDbContext dbContext) : base(dbContext)
    {
    }
}