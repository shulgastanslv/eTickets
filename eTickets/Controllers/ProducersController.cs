﻿using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class ProducersController : Controller
{
    private readonly IProducersService _producersService;

    public ProducersController(IProducersService producersService)
    {
        _producersService = producersService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _producersService.GetAllAsync();
        return View(data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var data = await _producersService.GetByIdAsync(id);

        return View(data);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var data = await _producersService.GetByIdAsync(id);

        return View(data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _producersService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("ProfilePictureURL, FullName, Bio")] Producer producer)
    {
        if (ModelState.IsValid)
        {
            return View(producer);
        }

        await _producersService.AddAsync(producer);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var data = await _producersService.GetByIdAsync(id);

        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id, ProfilePictureURL, FullName, Bio")] Producer producer)
    {
        await _producersService.UpdateAsync(id, producer);

        return RedirectToAction(nameof(Index));
    }

}