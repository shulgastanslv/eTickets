using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
{
    private readonly AppDbContext _appDbContext;

    public MoviesService(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }
    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var movie = await _appDbContext.Movies.Include(i => i.Cinema)
            .Include(i => i.Producer)
            .Include(i => i.Actors_Movies).ThenInclude(i => i.Actor).FirstOrDefaultAsync(i => i.Id == id);

        return movie;
    }

    public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
    {
        var response = new NewMovieDropdownsVM();
        response.Actors = await _appDbContext.Actors.OrderBy(i => i.FullName).ToListAsync();
        response.Cinemas = await _appDbContext.Cinemas.OrderBy(i => i.Name).ToListAsync();
        response.Producers = await _appDbContext.Producers.OrderBy(i => i.FullName).ToListAsync();

        return response;
    }

    public async Task AddNewMovie(NewMovieVM movie)
    {
        var newMovie = new Movie();
        newMovie.Name = movie.Name;
        newMovie.Description = movie.Description;
        newMovie.Price = movie.Price;
        newMovie.ImageURL = movie.ImageURL;
        newMovie.CinemaId = movie.CinemaId;
        newMovie.ProducerId = movie.ProducerId;
        newMovie.StartDate = movie.StartDate;
        newMovie.EndDate = movie.EndDate;
        newMovie.MovieCategory = movie.MovieCategory;

        _appDbContext.Movies.Add(newMovie);

        await _appDbContext.SaveChangesAsync();

        foreach (var actorId in movie.ActorIds)
        {
            var newActor = new Actor_Movie()
            {
                MovieId = newMovie.Id,
                ActorId = actorId,
            };

            await _appDbContext.Actors_Movies.AddAsync(newActor);
        }

        await _appDbContext.SaveChangesAsync();
    }
}