using CarRental.Domain.Entities;

namespace CarRental.Business.Services.Interfaces;

public interface ICarService
{
    Task<List<Car>> GetAvailableCarsAsync();
    Task<List<Car>> SearchAvailableCarsAsync(string? location, DateTime? startDate, DateTime? endDate, string? make);
}