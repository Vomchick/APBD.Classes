using Tutorial7.Contracts.Responses;
using Tutorial7.Entities;

namespace Tutorial7.Mappers;

public static class ClientMapperExtension
{
    public static GetAllClientTripsResponse ToResponse(this ClientTrip clientTrip)
    {
        return new GetAllClientTripsResponse
        {
            Id = clientTrip.IdTrip,
            Name = clientTrip.Name,
            Description = clientTrip.Description,
            DateFrom = clientTrip.DateFrom,
            DateTo = clientTrip.DateTo,
            MaxPeople = clientTrip.MaxPeople,
            RegisteredAt = clientTrip.RegisteredAt,
            PaymentDate = clientTrip.PaymentDate,
            Countries = clientTrip.Countries.Select(country => new CountryResponse(country.Id, country.Name)).ToList()
        };
    }

    public static ICollection<GetAllClientTripsResponse> ToResponse(
        this ICollection<ClientTrip> clientTrips)
    {
        return clientTrips.Select(x => x.ToResponse()).ToList();
    }
}
