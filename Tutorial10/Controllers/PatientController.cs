using Microsoft.AspNetCore.Mvc;
using Tutorial10.Application.DTO;
using Tutorial10.Application.Exceptions;
using Tutorial10.Application.Services.Interfaces;

namespace Tutorial10.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{patientId:int}")]
    [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AssignClienttoTrip([FromRoute] int patientId, CancellationToken token = default)
    {
        try
        {
            var patient = await _patientService.GetPatientAsync(patientId, token);
            return Ok(patient);
        }
        catch (PatientNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
