using Tutorial10.Core.DataModels;

namespace Tutorial10.Application.Repositories;

public interface IPrescriptionRepository
{
    Task<Prescription> AddAsync(Prescription prescription, ICollection<PrescriptionMedicament> prescriptionMedicaments, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
