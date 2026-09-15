using CarRental.Business.Services.Interfaces;
using CarRental.Data.Context;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Business.Services;

public class CarService : ICarService
{
    private readonly CarRentalDbContext _context;

    public CarService(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetAvailableCarsAsync()
    {
        return await _context.Cars.Where(car => car.IsAvailable).ToListAsync();
    }
    
    public async Task<List<Car>> SearchAvailableCarsAsync(string? location, DateTime? startDate, DateTime? endDate, string? make)
    {
        var query = _context.Cars.Where(car => car.IsAvailable).AsQueryable();
        if (!string.IsNullOrWhiteSpace(location))
        {
            query = query.Where(car => car.Location == location);
        }

        if (!string.IsNullOrWhiteSpace(make))
        {
            query = query.Where(car => car.Make == make);
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(car => !car.Reservations.Any(reservation =>
                    startDate.Value < reservation.EndDate && endDate.Value > reservation.StartDate));
        }

        return await query.ToListAsync();
    }
}