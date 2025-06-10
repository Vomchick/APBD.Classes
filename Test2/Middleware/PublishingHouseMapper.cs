using Test2.DTO;
using Test2.Models;

namespace Test2.Middleware;

public static class PublishingHouseMapper
{
    public static PublisherDto MapToDto(this PublishingHouse publisher)
    {
        if (publisher == null)
        {
            return null;
        }
        return new PublisherDto
        {
            Id = publisher.Id,
            Name = publisher.Name,
            Country = publisher.Country,
            City = publisher.City,
            Books = publisher.Books?.Select(b => b.MapToDto()).ToList()
        };
    }
}
