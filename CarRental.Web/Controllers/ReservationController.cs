using System.Security.Claims;
using CarRental.Business.Services.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateReservationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }
        
        if (model.StartDate.Date < DateTime.Today)
        {
            TempData["ErrorMessage"] = "Start date cannot be in the past.";
            return RedirectToAction("Index", "Home");
        }
        
        if (model.EndDate <= model.StartDate)
        {
            TempData["ErrorMessage"] = "End date must be after start date.";
            return RedirectToAction("Index", "Home");
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var isAvailable = await _reservationService.IsCarAvailableAsync(model.CarId, model.StartDate, model.EndDate);

        if (!isAvailable)
        {
            TempData["ErrorMessage"] = "This car is not available for the selected dates.";
            return RedirectToAction("Index", "Home");
        }

        var reservation = new Reservation
        {
            UserId = userId,
            CarId = model.CarId,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };

        await _reservationService.CreateReservationAsync(reservation);

        TempData["SuccessMessage"] = "Car booked successfully.";
        return RedirectToAction("Index", "Home");
    }
}