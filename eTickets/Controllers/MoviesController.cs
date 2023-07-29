using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class MoviesController : Controller
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _moviesService.GetAllAsync(i => i.Cinema);
        return View(data);
    }

    public async Task<IActionResult> Filter(string searchString)
    {
        var data = await _moviesService.GetAllAsync(i => i.Cinema);

        if (!string.IsNullOrEmpty(searchString))
        {
            var filteredResult = data.Where(i => i.Name.Contains(searchString) || i.Description.Contains(searchString)).ToList();

            return View("Index", filteredResult);
        }

        return View("Index", data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movieDetails = await _moviesService.GetMovieByIdAsync(id);

        return View(movieDetails);
    }
    public async Task<IActionResult> Create()
    {
        var movieDropdownData = await _moviesService.GetNewMovieDropdownsValues();

        ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewMovieVM movie)
    {
        await _moviesService.AddNewMovie(movie);

        var movieDropdownData = await _moviesService.GetNewMovieDropdownsValues();

        ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");

        return RedirectToAction(nameof(Index));
    }





}