using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarRental.Web.Models;
using CarRental.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarService _carService;

    public HomeController(
        ILogger<HomeController> logger,
        ICarService carService)
    {
        _logger = logger;
        _carService = carService;
    }

    [Authorize]
    public async Task<IActionResult> Index(string? location, DateTime? startDate, DateTime? endDate, string? make)
    {
        var model = new CarSearchViewModel
        {
            Location = location,
            StartDate = startDate,
            EndDate = endDate,
            Make = make
        };

        if (startDate.HasValue != endDate.HasValue)
        {
            ModelState.AddModelError(nameof(model.EndDate), "Both start date and end date are required when searching by date.");
            model.Cars = await _carService.GetAvailableCarsAsync();

            return View(model);
        }
        
        if (startDate.HasValue && endDate.HasValue && endDate.Value <= startDate.Value)
        {
            ModelState.AddModelError(nameof(model.EndDate), "End date must be after start date.");
            model.Cars = await _carService.GetAvailableCarsAsync();

            return View(model);
        }

        model.Cars = await _carService.SearchAvailableCarsAsync(location, startDate, endDate, make);
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}