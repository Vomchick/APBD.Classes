using Tutorial7.Contracts.Responses;
using Tutorial7.Entities;

namespace Tutorial7.Mappers;

public static class TripMapperExtension
{
    public static GetAllTripsResponse ToResponse(this Trip trip)
    {
        return new GetAllTripsResponse
        {
            Id = trip.Id,
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople,
            Countries = trip.Countries.Select(country => new CountryResponse(country.Id, country.Name)).ToList()
        };
    }

    public static ICollection<GetAllTripsResponse> ToResponse(this ICollection<Trip> trips)
    {
        return [.. trips.Select(x => x.ToResponse())];
    }
}
