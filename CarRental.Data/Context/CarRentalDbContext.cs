using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Context;

public class CarRentalDbContext : DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
        modelBuilder.Entity<Car>().Property(car => car.PricePerDay).HasPrecision(10, 2);
        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corolla",
                Year = 2024,
                Location = "Nablus",
                PricePerDay = 40.00m,
                IsAvailable = true
            },
            new Car
            {
                Id = 2,
                Make = "Kia",
                Model = "Sportage",
                Year = 2023,
                Location = "Ramallah",
                PricePerDay = 65.00m,
                IsAvailable = true
            },
            new Car
            {
                Id = 3,
                Make = "Hyundai",
                Model = "Elantra",
                Year = 2022,
                Location = "Nablus",
                PricePerDay = 45.00m,
                IsAvailable = true
            },
            new Car
            {
                Id = 4,
                Make = "BMW",
                Model = "X5",
                Year = 2024,
                Location = "Ramallah",
                PricePerDay = 120.00m,
                IsAvailable = false
            }
        );
    }
}