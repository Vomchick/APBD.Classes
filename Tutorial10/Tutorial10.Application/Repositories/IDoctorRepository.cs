using Tutorial10.Core.DataModels;

namespace Tutorial10.Application.Repositories;

public interface IDoctorRepository
{
    Task<Doctor?> GetDoctorAsync(int id, CancellationToken cancellationToken);
}
