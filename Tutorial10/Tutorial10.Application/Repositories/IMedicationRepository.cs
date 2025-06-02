using Tutorial10.Core.Models;

namespace Tutorial10.Application.Repositories;

public interface IMedicationRepository
{
    Task<Medicament?> GetMedicamentAsync(int id, CancellationToken cancellationToken);
}
