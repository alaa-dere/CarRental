using System.ComponentModel.DataAnnotations;

namespace CarRental.Web.Models;

public class RegisterViewModel
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [CustomValidation(typeof(RegisterViewModel), nameof(ValidateDateOfBirth))]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    [Required]
    public string DriverLicenseNumber { get; set; } = string.Empty;
    
    public static ValidationResult? ValidateDateOfBirth(DateTime? dateOfBirth, ValidationContext context)
    {
        if (dateOfBirth == null)
        {
            return ValidationResult.Success;
        }

        if (dateOfBirth.Value.Date > DateTime.Today)
        {
            return new ValidationResult("Date of birth cannot be in the future.");
        }

        return ValidationResult.Success;
    }
}