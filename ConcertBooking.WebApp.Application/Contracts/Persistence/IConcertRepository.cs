using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Application.Contracts.Persistence;

public interface IConcertRepository : IAsyncRepository<Concert>
{
    
}