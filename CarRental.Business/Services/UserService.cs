using CarRental.Business.Services.Interfaces;
using CarRental.Data.Context;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Business.Services;

public class UserService : IUserService
{
    private readonly CarRentalDbContext _context;

    public UserService(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email == email);
    }

    public async Task RegisterUserAsync(User user, string password)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}