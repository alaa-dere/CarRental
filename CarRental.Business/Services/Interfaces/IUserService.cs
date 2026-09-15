using CarRental.Domain.Entities;

namespace CarRental.Business.Services.Interfaces;

public interface IUserService
{
    Task<bool> EmailExistsAsync(string email);
    Task RegisterUserAsync(User user, string password);
    Task<User?> ValidateCredentialsAsync(string email, string password);
}