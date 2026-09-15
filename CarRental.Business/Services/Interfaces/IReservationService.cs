using CarRental.Domain.Entities;

namespace CarRental.Business.Services.Interfaces;

public interface IReservationService
{
    Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate);
    Task CreateReservationAsync(Reservation reservation);
}