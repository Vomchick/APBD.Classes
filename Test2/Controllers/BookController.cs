using Microsoft.AspNetCore.Mvc;
using Test2.DTO;
using Test2.Exceptions;
using Test2.Services.Core;

namespace Test2.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService testService)
    {
        _bookService = testService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBook([FromBody] AddBookDto dto)
    {
        try
        {
            await _bookService.CreateBook(dto);
            return Created();
        }
        catch (Exception ex) when (
            ex is AuthorNotFoundException ||
            ex is PublishingHouseNotFoundException
        )
        {
            return NotFound(ex.Message);
        }
    }
}
