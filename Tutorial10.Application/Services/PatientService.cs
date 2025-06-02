using Tutorial10.Application.DTO;
using Tutorial10.Application.Exceptions;
using Tutorial10.Application.Mappers;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Services.Interfaces;

namespace Tutorial10.Application.Services;

public class PatientService : IPatientService
{
    public readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDto> GetPatientAsync(int id, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetPatientAsync(id, cancellationToken);
        if (patient == null)
        {
            throw new PatientNotFoundException(id);
        }
        return patient.ToPatientDto();
    }
}
