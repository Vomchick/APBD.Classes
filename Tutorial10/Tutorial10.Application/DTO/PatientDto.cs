namespace Tutorial10.Application.DTO;

public class PatientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }

    public ICollection<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();
}
