using Tutorial10.Application.DTO;
using Tutorial10.Core.Models;

namespace Tutorial10.Application.Mappers;

public static class PrescriptionMapper
{
    public static PrescriptionDto ToPrescriptionDto(this Prescription prescription)
    {
        return new PrescriptionDto
        {
            Id = prescription.Id,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new DoctorDto
            {
                Id = prescription.Doctor.Id,
                FirstName = prescription.Doctor.FirstName,
                LastName = prescription.Doctor.LastName,
                Email = prescription.Doctor.Email
            },
            Medicaments = prescription.PrescriptionMedicaments.Select(pm => new MedicamentDto
            {
                Id = pm.MedicamentId,
                Name = pm.Medicament.Name,
                Dose = pm.Dose,
                Description = pm.Medicament.Description,
                Type = pm.Medicament.Type,
            }).ToList(),
        };
    }
}
