using eTickets.Data;
using eTickets.Data.Services;
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

    public async Task<IActionResult> Index()
    {
        var data = await _cinemasService.GetAllAsync();
        return View(data);
    }
}