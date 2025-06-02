namespace Tutorial10.Application.DTO;

public class AddPrescriptionDto
{
    public AddPatientDto Patient { get; set; } = new AddPatientDto();
    public ICollection<MedicamentPrescriptionDto> Medicaments { get; set; } = new List<MedicamentPrescriptionDto>();
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int DoctorId { get; set; }
}
