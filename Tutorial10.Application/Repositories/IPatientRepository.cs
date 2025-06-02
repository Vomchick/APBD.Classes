using Tutorial10.Core.Models;

namespace Tutorial10.Application.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetPatientAsync(int id, CancellationToken cancellationToken);
    Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken);
}
