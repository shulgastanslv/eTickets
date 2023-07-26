using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class ActorsService : IActorsService
{
    private readonly AppDbContext _appDbContext;
    public ActorsService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        var data = await _appDbContext.Actors.ToListAsync();

        return data;
    }

    public async Task<Actor> GetByIdAsync(int id)
    {
        var result = await _appDbContext.Actors.FirstOrDefaultAsync(i => i.Id == id);

        return result;
    }

    public async Task AddAsync(Actor actor)
    {
        _appDbContext.Actors.Add(actor);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Actor> UpdateAsync(int id, Actor newActor)
    {
        _appDbContext.Update(newActor);
        await _appDbContext.SaveChangesAsync();
        return newActor;
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _appDbContext.Actors.FirstOrDefaultAsync(i => i.Id == id);
        _appDbContext.Actors.Remove(result);
        await _appDbContext.SaveChangesAsync();
    }
}