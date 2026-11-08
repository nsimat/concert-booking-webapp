using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Application.Contracts.Persistence;

public interface ICustomerRepository : IAsyncRepository<Customer>
{
    // Retrieve a customer by their associated application user ID
    Task<Customer?> GetByApplicationUserIdAsync(Guid applicationUserId, CancellationToken cancellationToken = default);
}