using Tutorial10.Application.DTO;

namespace Tutorial10.Application.Services.Interfaces;

public interface IPatientService
{
    Task<PatientDto> GetPatientAsync(int id, CancellationToken cancellationToken);
}
