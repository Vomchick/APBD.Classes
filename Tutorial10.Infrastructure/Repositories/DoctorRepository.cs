using Microsoft.EntityFrameworkCore;
using Tutorial10.Application.Repositories;
using Tutorial10.Core.Models;

namespace Tutorial10.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ClinicContext _context;

    public DoctorRepository(ClinicContext context)
    {
        _context = context;
    }
    
    public async Task<Doctor?> GetDoctorAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
}
