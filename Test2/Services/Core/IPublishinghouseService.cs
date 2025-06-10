using Test2.DTO;

namespace Test2.Services.Core;

public interface IPublishinghouseService
{
    Task<List<PublisherDto>> GetPublishingHouses(string? city, string? country);
}
