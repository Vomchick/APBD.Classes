using Microsoft.VisualBasic;
using Tutorial10.Application.DTO;
using Tutorial10.Application.Exceptions;
using Tutorial10.Application.Mappers;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Services.Interfaces;
using Tutorial10.Core.DataModels;

namespace Tutorial10.Application.Services;

public class PrescriptionService : IPrescriptionService
{
    public readonly IPrescriptionRepository _prescriptionRepository;
    public readonly IMedicationRepository _medicationRepository;
    public readonly IDoctorRepository _doctorRepository;
    public readonly IPatientRepository _patientRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository, IMedicationRepository medicationRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _medicationRepository = medicationRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    public async Task IssuePrescription(AddPrescriptionDto prescriptionRequest, CancellationToken token)
    {
        var doctor = await _doctorRepository.GetDoctorAsync(prescriptionRequest.DoctorId, token);
        if (doctor == null)
        {
            throw new DoctorNotFoundException(prescriptionRequest.DoctorId);
        }

        var patient = await _patientRepository.GetPatientAsync(prescriptionRequest.Patient.Id ?? 0, token);
        if (patient == null)
        {
            patient = await _patientRepository.AddAsync(prescriptionRequest.Patient.ToPatient(), token);
        }

        if(prescriptionRequest.Medicaments.Count > 10)
        {
            throw new TooManyMedicamentsException(prescriptionRequest.Medicaments.Count);
        }

        if(prescriptionRequest.DueDate < prescriptionRequest.Date)
        {
            throw new InvalidDueDateException(prescriptionRequest.Date, prescriptionRequest.DueDate);
        }

        var prescription = new Prescription
        {
            Doctor = doctor,
            Patient = patient,
            Date = prescriptionRequest.Date,
            DueDate = prescriptionRequest.DueDate
        };

        var medicamentPrescriptions = new List<PrescriptionMedicament>(prescriptionRequest.Medicaments.Count);
        foreach (var medicationInformation in prescriptionRequest.Medicaments)
        {
            var medication = await _medicationRepository.GetMedicamentAsync(medicationInformation.MedicamentId, token);
            if (medication == null)
            {
                throw new MedicationNotFoundException(medicationInformation.MedicamentId);
            }

            medicamentPrescriptions.Add(new PrescriptionMedicament
            {
                Medicament = medication,
                Prescription = prescription,
                Dose = medicationInformation.Dose,
                Details = medicationInformation.Details
            });
        }

        await _prescriptionRepository.AddAsync(prescription, medicamentPrescriptions, token);
        await _prescriptionRepository.SaveChangesAsync(token);
    }
}
