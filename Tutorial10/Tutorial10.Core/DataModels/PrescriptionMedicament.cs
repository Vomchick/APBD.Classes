namespace Tutorial10.Core.DataModels;

public class PrescriptionMedicament
{
    public int MedicamentId { get; set; }
    public int PrescriptionId { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }

    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
}
