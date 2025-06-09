using Tutorial10.Application.DTO;
using Tutorial10.Core.DataModels;

namespace Tutorial10.Application.Mappers;

public static class PatientMapper
{
    public static Patient ToPatient(this AddPatientDto dto)
    {
        return new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BirthDate = dto.BirthDate
        };
    }

    public static PatientDto ToPatientDto(this Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,

            Prescriptions = patient.Prescriptions.Select(x => x.ToPrescriptionDto()).ToList()
        };
    }
}
