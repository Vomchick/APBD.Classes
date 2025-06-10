namespace Test2.DTO;

public class AddBookDto
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int PublishingHouseId { get; set; }
    public List<int> AuthorIds { get; set; } = new List<int>();
    public List<GenreDto> Genres { get; set; } = new List<GenreDto>();
}
