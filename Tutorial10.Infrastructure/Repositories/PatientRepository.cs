using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Repositories;
using Tutorial10.Core.Models;

namespace Tutorial10.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ClinicContext _context;

    public PatientRepository(ClinicContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetPatientAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Patients
            .Include(x => x.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(x => x.Prescriptions)
            .ThenInclude(d => d.Doctor)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        await _context.Patients.AddAsync(patient, cancellationToken);
        return patient;
    }
}
