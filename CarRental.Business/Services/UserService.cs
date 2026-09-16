using CarRental.Business.Services.Interfaces;
using CarRental.Data.Context;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Business.Services;

public class UserService : IUserService
{
    private readonly CarRentalDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(CarRentalDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email == email);
    }

    public async Task RegisterUserAsync(User user, string password)
    {
        user.PasswordHash = _passwordHasher.HashPassword(user, password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (user == null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }
    
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<bool> EmailExistsForAnotherUserAsync(string email, int userId)
    {
        return await _context.Users.AnyAsync(user => user.Email == email && user.Id != userId);
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}