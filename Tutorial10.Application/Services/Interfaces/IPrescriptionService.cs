using Tutorial10.Application.DTO;

namespace Tutorial10.Application.Services.Interfaces;

public interface IPrescriptionService
{
    Task IssuePrescription(AddPrescriptionDto prescriptionRequest, CancellationToken token);
}
