using CarRental.Domain.Entities;

namespace CarRental.Business.Services.Interfaces;

public interface IUserService
{
    Task<bool> EmailExistsAsync(string email);
    Task RegisterUserAsync(User user, string password);
    Task<User?> ValidateCredentialsAsync(string email, string password);
    Task<User?> GetUserByIdAsync(int userId);
    Task<bool> EmailExistsForAnotherUserAsync(string email, int userId);
    Task UpdateUserAsync(User user);
}