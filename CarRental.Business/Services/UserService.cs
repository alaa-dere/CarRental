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
}