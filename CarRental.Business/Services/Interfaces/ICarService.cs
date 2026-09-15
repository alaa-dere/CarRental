using CarRental.Domain.Entities;

namespace CarRental.Business.Services.Interfaces;

public interface ICarService
{
    Task<List<Car>> GetAvailableCarsAsync();
}