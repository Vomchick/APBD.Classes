using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Test2.Context;
using Test2.DTO;
using Test2.Exceptions;
using Test2.Middleware;
using Test2.Models;
using Test2.Services.Core;

namespace Test2.Services;

public class BookService : IBookService
{
    private readonly MyAppContext _appContext;

    public BookService(MyAppContext appContext)
    {
        _appContext = appContext;
    }
    public async Task CreateBook(AddBookDto dto)
    {
        foreach (var authorId in dto.AuthorIds)
        {
            if (!await _appContext.Authors.AnyAsync(x => x.Id == authorId))
            {
                throw new AuthorNotFoundException(authorId);
            }
        }

        var publishingHouse = await _appContext.PublishingHouses.FirstOrDefaultAsync(x => x.Id == dto.PublishingHouseId)
            ?? throw new PublishingHouseNotFoundException(dto.PublishingHouseId);

        foreach (var genre in dto.Genres)
        {
            if (!await _appContext.Genres.AnyAsync(x => x.Name == genre.Name))
            {
                var newGenre = new Genre
                {
                    Name = genre.Name
                };
                await _appContext.Genres.AddAsync(newGenre);
            }
        }
        await _appContext.SaveChangesAsync();

        var book = new Book
        {
            Name = dto.Name,
            ReleaseDate = dto.ReleaseDate,
            PublishingHouseId = dto.PublishingHouseId,
            Authors = await _appContext.Authors.Where(x => dto.AuthorIds.Contains(x.Id)).ToListAsync(),
            Genres = await _appContext.Genres.Where(x => dto.Genres.Select(g => g.Name).Contains(x.Name)).ToListAsync()
        };

        await _appContext.Books.AddAsync(book);
        await _appContext.SaveChangesAsync();
    }
}
