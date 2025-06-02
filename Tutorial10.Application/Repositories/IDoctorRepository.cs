using Tutorial10.Core.Models;

namespace Tutorial10.Application.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetDoctorAsync(int id, CancellationToken cancellationToken);
}
