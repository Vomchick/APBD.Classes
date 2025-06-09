using Tutorial10.Core.DataModels;

namespace Tutorial10.Application.Repositories;

public interface IMedicationRepository
{
    Task<Medicament?> GetMedicamentAsync(int id, CancellationToken cancellationToken);
}
