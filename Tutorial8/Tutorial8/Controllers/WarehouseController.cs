using Microsoft.AspNetCore.Mvc;
using Tutorial8.Contracts.Requests;
using Tutorial8.Exceptions;
using Tutorial8.Services.Core;

namespace Tutorial8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _service;

    public WarehouseController(IWarehouseService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddProduct([FromBody] ProductWarehouseRequest request)
    {
        try
        {
            int id = await _service.AddProductToWarehouse(request);
            return Ok(new { IdProductWarehouse = id });
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (WarehouseNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (InvalidAmountException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (OrderAlreadyFulfilledException ex)
        {
            return Conflict(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred", Details = ex.Message });
        }
    }

    [HttpPost("stored")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProductUsingProcedure([FromBody] ProductWarehouseRequest request)
    {
        try
        {
            int id = await _service.AddProductToWarehouseWithProc(request);
            return Ok(new { IdProductWarehouse = id });
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = e.Message });
        }
    }
}

