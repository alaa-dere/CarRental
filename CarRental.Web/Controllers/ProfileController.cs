using System.Security.Claims;
using CarRental.Business.Services.Interfaces;
using CarRental.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserService _userService;

    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            DateOfBirth = user.DateOfBirth,
            AddressLine1 = user.AddressLine1,
            AddressLine2 = user.AddressLine2,
            City = user.City,
            Country = user.Country,
            DriverLicenseNumber = user.DriverLicenseNumber
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var emailExists = await _userService.EmailExistsForAnotherUserAsync(model.Email, userId);

        if (emailExists)
        {
            ModelState.AddModelError(nameof(model.Email), "An account with this email already exists.");
            return View(model);
        }

        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.Phone = model.Phone;
        user.DateOfBirth = model.DateOfBirth;
        user.AddressLine1 = model.AddressLine1;
        user.AddressLine2 = model.AddressLine2;
        user.City = model.City;
        user.Country = model.Country;
        user.DriverLicenseNumber = model.DriverLicenseNumber;

        await _userService.UpdateUserAsync(user);
        TempData["SuccessMessage"] = "Profile updated successfully.";

        return RedirectToAction(nameof(Index));
    }
}