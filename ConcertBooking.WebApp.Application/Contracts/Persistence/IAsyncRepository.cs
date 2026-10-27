namespace ConcertBooking.WebApp.Application.Contracts.Persistence;

/// <summary>
/// Defines a generic repository interface for performing asynchronous operations on entities of the specified type.
/// </summary>
/// <typeparam name="T">The type of the entity managed by the repository. Must be a class.</typeparam>
public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}