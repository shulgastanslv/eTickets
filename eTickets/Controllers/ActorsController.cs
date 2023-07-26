using System.Runtime.CompilerServices;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class ActorsController : Controller
{
    private readonly IActorsService _actorsService;

    public ActorsController(IActorsService actorsService)
    {
        _actorsService = actorsService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _actorsService.GetAllAsync();
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Details(int id)
    {
        var data = await _actorsService.GetByIdAsync(id);

        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Actor actor)
    {
        if (ModelState.IsValid)
        {
            return View(actor);
        }

        await _actorsService.AddAsync(actor);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var data = await _actorsService.GetByIdAsync(id);

        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")] Actor actor)
    {
        await _actorsService.UpdateAsync(id, actor);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var data = await _actorsService.GetByIdAsync(id);

        return View(data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _actorsService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}