namespace CarRental.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Location { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; }
}