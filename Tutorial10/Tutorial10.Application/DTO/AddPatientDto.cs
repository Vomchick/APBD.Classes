using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Application.DTO;

public class AddPatientDto
{
    public int? Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }
}
