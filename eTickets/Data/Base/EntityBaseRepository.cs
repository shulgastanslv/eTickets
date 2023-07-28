using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Data.Base;

public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext _appDbContext;
    public EntityBaseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return (await _appDbContext.Set<T>().FirstOrDefaultAsync(i => i.Id == id))!;
    }

    public async Task AddAsync(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, T newEntity)
    {
        EntityEntry entityEntry = _appDbContext.Entry<T>(newEntity);
        entityEntry.State = EntityState.Modified;

        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _appDbContext.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
        EntityEntry entityEntry = _appDbContext.Entry<T>(entity);
        entityEntry.State = EntityState.Deleted;

        await _appDbContext.SaveChangesAsync();
    }
}