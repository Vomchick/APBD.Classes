using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Tutorial10.Core.Models;

namespace Tutorial10.Infrastructure.Extensions;

internal static class ModelBuilderExtension
{
    public static void AddInitialData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient { Id = 1, FirstName = "Alice", LastName = "Smith", BirthDate = new DateTime(1990, 5, 1) }
        );

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { Id = 1, Name = "Aspirin", Description = "Pain reliever", Type = "Tablet" },
            new Medicament { Id = 2, Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "Tablet" }
        );

        modelBuilder.Entity<Prescription>().HasData(
            new Prescription
            {
                Id = 1,
                Date = new DateTime(2025, 6, 2),
                DueDate = new DateTime(2025, 6, 12),
                DoctorId = 1,
                PatientId = 1
            }
        );

        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament
            {
                PrescriptionId = 1,
                MedicamentId = 1,
                Dose = 2,
                Details = "Take after meals"
            },
            new PrescriptionMedicament
            {
                PrescriptionId = 1,
                MedicamentId = 2,
                Dose = null,
                Details = "Take before sleep"
            }
        );
    }
}
