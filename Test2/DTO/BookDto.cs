using Test2.Models;

namespace Test2.DTO;

public class BookDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }

    public ICollection<AuthorDto> Authors { get; set; }
    public ICollection<GenreDto> Genres { get; set; }
}
