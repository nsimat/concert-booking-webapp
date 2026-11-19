using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Entities;
using ConcertBooking.WebApp.Persistence.Data;

namespace ConcertBooking.WebApp.Persistence.Repositories;

public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
{
    public ConcertRepository(ConcertBookingDbContext dbContext) : base(dbContext)
    {
    }
}