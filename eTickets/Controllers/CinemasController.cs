using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class CinemasController : Controller
{
    private readonly ICinemasService _cinemasService;

    public CinemasController(ICinemasService cinemasService)
    {
        _cinemasService = cinemasService;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Logo, Name, Description")] Cinema cinema)
    {
        if (ModelState.IsValid)
        {
            return View(cinema);
        }

        await _cinemasService.AddAsync(cinema);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Index()
    {
        var data = await _cinemasService.GetAllAsync();
        return View(data);
    }


    public async Task<IActionResult> Details(int id)
    {
        var data = await _cinemasService.GetByIdAsync(id);

        return View(data);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var data = await _cinemasService.GetByIdAsync(id);

        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Logo, Name, Description")] Cinema cinema)
    {
        await _cinemasService.UpdateAsync(id, cinema);

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        var data = await _cinemasService.GetByIdAsync(id);

        return View(data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _cinemasService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}