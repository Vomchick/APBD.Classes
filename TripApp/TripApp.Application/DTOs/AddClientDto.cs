using System.ComponentModel.DataAnnotations;

namespace TripApp.Application.DTOs;

public class AddClientDto
{
    [Required]
    public required string FisrstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Telephone { get; set; } = string.Empty;

    [Required]
    public string Pesel { get; set; } = string.Empty;


    public DateTime? PaymentDate { get; set; }
}
