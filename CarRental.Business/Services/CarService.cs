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
}