using System.ComponentModel.DataAnnotations;

namespace CarRental.Web.Models;

public class CreateReservationViewModel
{
    [Required]
    public int CarId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
}