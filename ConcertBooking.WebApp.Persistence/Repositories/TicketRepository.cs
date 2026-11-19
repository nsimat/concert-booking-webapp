using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Entities;
using ConcertBooking.WebApp.Persistence.Data;

namespace ConcertBooking.WebApp.Persistence.Repositories;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
    public TicketRepository(ConcertBookingDbContext dbContext) : base(dbContext)
    {
    }
}