using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ConcertBooking.WebApp.Persistence.Repositories;

public class BaseRepository<T>(ConcertBookingDbContext dbContext) : IAsyncRepository<T>
    where T : class
{
    protected ConcertBookingDbContext DbContext { get; } = dbContext;

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        T? entity = await DbContext.Set<T>().FindAsync(id, cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<T>().AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync(cancellationToken);    

        return entity;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}