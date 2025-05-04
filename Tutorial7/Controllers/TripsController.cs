using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorial7.Contracts.Responses;
using Tutorial7.Mappers;
using Tutorial7.Services.Core;

namespace Tutorial7.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<GetAllTripsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTripsAsync(CancellationToken token)
        {
            var trips = await _tripService.GetAllTripsWithCountriesAsync(token);

            return Ok(trips.ToResponse());
        }
    }
}
