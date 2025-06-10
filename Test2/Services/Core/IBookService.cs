using Test2.DTO;
namespace Test2.Services.Core;

public interface IBookService
{
    Task CreateBook(AddBookDto dto);
}
