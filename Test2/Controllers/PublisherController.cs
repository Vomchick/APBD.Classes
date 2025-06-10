using Microsoft.AspNetCore.Mvc;
using Test2.DTO;
using Test2.Services.Core;

namespace Test2.Controllers;

[ApiController]
[Route("api/publishing_houses")]
public class PublisherController : ControllerBase
{
    private readonly IPublishinghouseService _publisherService;

    public PublisherController(IPublishinghouseService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<PublisherDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHouses([FromQuery] string? country, [FromQuery] string? city)
    {
        var houses = await _publisherService.GetPublishingHouses(city, country);
        return Ok(houses);
    }
}
