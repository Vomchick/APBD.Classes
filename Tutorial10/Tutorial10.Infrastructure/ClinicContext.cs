using Microsoft.EntityFrameworkCore;
using Tutorial10.Core.DataModels;
using Tutorial10.Infrastructure.Extensions;

namespace Tutorial10.Infrastructure;

public class ClinicContext : DbContext
{
    public virtual DbSet<Patient> Patients { get; set; } = null!;
    public virtual DbSet<Doctor> Doctors { get; set; } = null!;
    public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;
    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
    public virtual DbSet<Medicament> Medicaments { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; }

    public ClinicContext(DbContextOptions<ClinicContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.MedicamentId, pm.PrescriptionId });

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.MedicamentId);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.PrescriptionId);

        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Prescriptions)
            .WithOne(pr => pr.Patient)
            .HasForeignKey(pr => pr.PatientId);

        modelBuilder.Entity<Doctor>()
            .HasMany(d => d.Prescriptions)
            .WithOne(pr => pr.Doctor)
            .HasForeignKey(pr => pr.DoctorId);

        modelBuilder.AddInitialData();

        base.OnModelCreating(modelBuilder);
    }
}
