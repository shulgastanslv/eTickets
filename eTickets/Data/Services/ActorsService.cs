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

    public Actor GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Actor actor)
    {
        throw new NotImplementedException();
    }

    public Actor Update(int id, Actor newActor)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}