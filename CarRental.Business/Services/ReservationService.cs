using CarRental.Business.Services.Interfaces;
using CarRental.Data.Context;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Business.Services;

public class ReservationService : IReservationService
{
    private readonly CarRentalDbContext _context;

    public ReservationService(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == carId);

        if (car == null || !car.IsAvailable)
        {
            return false;
        }

        var hasOverlappingReservation =
            await _context.Reservations.AnyAsync(reservation =>
                reservation.CarId == carId &&
                startDate < reservation.EndDate &&
                endDate > reservation.StartDate);

        return !hasOverlappingReservation;
    }

    public async Task CreateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Add(reservation);

        await _context.SaveChangesAsync();
    }
}