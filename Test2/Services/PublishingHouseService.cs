using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Test2.Context;
using Test2.DTO;
using Test2.Middleware;
using Test2.Services.Core;

namespace Test2.Services;

public class PublishingHouseService : IPublishinghouseService
{
    private readonly MyAppContext _appContext;

    public PublishingHouseService(MyAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<PublisherDto>> GetPublishingHouses(string? city, string? country)
    {
        var houses = _appContext.PublishingHouses
            .Include(x => x.Books)
            .ThenInclude(b => b.Authors)
            .Include(x => x.Books)
            .ThenInclude(x => x.Genres)
            .AsQueryable();

        if (!city.IsNullOrEmpty())
        {
            houses = houses.Where(x => x.City == city);
        }
        if (!country.IsNullOrEmpty())
        {
            houses = houses.Where(x => x.Country == country);
        }

        houses = houses.OrderBy(x => x.Country).ThenBy(x => x.Name);

        return await houses.Select(x => x.MapToDto()).ToListAsync();
    }
}
