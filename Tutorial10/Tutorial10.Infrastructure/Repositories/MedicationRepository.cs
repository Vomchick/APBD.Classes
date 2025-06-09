using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Repositories;
using Tutorial10.Core.DataModels;

namespace Tutorial10.Infrastructure.Repositories;

public class MedicationRepository : IMedicationRepository
{
    private readonly ClinicContext _context;

    public MedicationRepository(ClinicContext context)
    {
        _context = context;
    }

    public async Task<Medicament?> GetMedicamentAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Medicaments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
}
