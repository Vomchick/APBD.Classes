using Test2.Models;

namespace Test2.DTO;

public class PublisherDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }

    public ICollection<BookDto> Books { get; set; }
}
