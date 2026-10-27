using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Application.Contracts.Persistence;

public interface IVenueRepository : IAsyncRepository<Venue>
{
    
}