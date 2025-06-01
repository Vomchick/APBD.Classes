using Microsoft.AspNetCore.Mvc;
using TripApp.Application.DTOs;
using TripApp.Application.Exceptions;
using TripApp.Application.Mappers;
using TripApp.Application.Services.Interfaces;
using TripApp.Core.Model;

namespace Trip.API.Controllers;

[ApiController]
[Route("api/trips")]
public class TripController(
    ITripService tripService) 
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<GetTripDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<GetTripDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrips(
        [FromQuery(Name = "page")] int? page,
        [FromQuery(Name = "pageSize")] int? pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page is null && pageSize is null)
        {
            var trips = await tripService.GetAllTripsAsync();
            return Ok(trips);
        }

        var paginatedTrips = await tripService.GetPaginatedTripsAsync(page ?? 1, pageSize ?? 10);
        return Ok(paginatedTrips);
    }

    [HttpPost("{tripId:int}/clients")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AssignClienttoTrip([FromRoute] int tripId, [FromBody] AddClientDto request, CancellationToken token = default)
    {
        try
        {
            await tripService.AddClientToTrip(tripId, request.MapToClient(), request.PaymentDate, token);
            return Created();
        }
        catch (ClientExceptions.PeselAlreadyAssignedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (TripExceptions.TripNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (TripExceptions.TripAlreadyOccuredException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}