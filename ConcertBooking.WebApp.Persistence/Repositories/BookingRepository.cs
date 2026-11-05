using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Persistence.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(ConcertBookingDbContext dbContext) : base(dbContext)
    {
    }
}