using Tutorial10.Core.Models;

namespace Tutorial10.Application.Repositories;

public interface IPrescriptionRepository
{
    Task<Prescription> AddAsync(Prescription prescription, ICollection<PrescriptionMedicament> prescriptionMedicaments, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
