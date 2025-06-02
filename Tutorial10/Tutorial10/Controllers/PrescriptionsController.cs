using Microsoft.AspNetCore.Mvc;
using Tutorial10.Application.DTO;
using Tutorial10.Application.Exceptions;
using Tutorial10.Application.Services.Interfaces;

namespace Tutorial10.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        this.prescriptionService = prescriptionService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AssignClienttoTrip([FromBody] AddPrescriptionDto request, CancellationToken token = default)
    {
        try
        {
            await prescriptionService.IssuePrescription(request, token);
            return Created();
        }
        catch (Exception ex) when (
            ex is TooManyMedicamentsException ||
            ex is InvalidDueDateException
        )
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex) when (
            ex is DoctorNotFoundException ||
            ex is MedicationNotFoundException
        )
        {
            return NotFound(ex.Message);
        }
    }
}
