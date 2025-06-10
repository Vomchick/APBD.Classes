using Test2.DTO;
using Test2.Models;

namespace Test2.Middleware;

public static class BookMapper
{
    public static BookDto MapToDto(this Book book)
    {
        if (book == null)
        {
            return null;
        }

        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            ReleaseDate = book.ReleaseDate,
            Authors = book.Authors?.Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName
            }).ToList(),
            Genres = book.Genres?.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList()
        };
    }
}
