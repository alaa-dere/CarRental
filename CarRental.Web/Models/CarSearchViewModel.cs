using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Entities;

namespace CarRental.Web.Models;

public class CarSearchViewModel
{
    public string? Location { get; set; }

    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    public string? Make { get; set; }
    
    public List<Car> Cars { get; set; } = new();
}