using Tutorial10.Application.Repositories;
using Tutorial10.Core.Models;

namespace Tutorial10.Infrastructure.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ClinicContext _context;

    public PrescriptionRepository(ClinicContext context)
    {
        _context = context;
    }

    public async Task<Prescription> AddAsync(
        Prescription prescription, 
        ICollection<PrescriptionMedicament> prescriptionMedicaments, 
        CancellationToken cancellationToken)
    {
        await _context.Prescriptions.AddAsync(prescription, cancellationToken);
        await _context.PrescriptionMedicaments.AddRangeAsync(prescriptionMedicaments, cancellationToken);
        return prescription;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
