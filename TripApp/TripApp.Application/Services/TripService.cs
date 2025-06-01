using TripApp.Application.DTOs;
using TripApp.Application.Exceptions;
using TripApp.Application.Mappers;
using TripApp.Application.Repository;
using TripApp.Application.Services.Interfaces;
using TripApp.Core.Model;

namespace TripApp.Application.Services;

public class TripService(
    ITripRepository tripRepository, 
    IClientRepository clientRepository, 
    IClientTripRepository clientTripRepository, 
    IClock clock) 
    : ITripService
{
    public async Task<PaginatedResult<GetTripDto>> GetPaginatedTripsAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 10) pageSize = 10;
        var result = await tripRepository.GetPaginatedTripsAsync(page, pageSize);

        var mappedTrips = new PaginatedResult<GetTripDto>
        {
            AllPages = result.AllPages,
            Data = result.Data.Select(trip => trip.MapToGetTripDto()).ToList(),
            PageNum = result.PageNum,
            PageSize = result.PageSize
        };

        return mappedTrips;
    }

    public async Task<IEnumerable<GetTripDto>> GetAllTripsAsync()
    {
        var trips = await tripRepository.GetAllTripsAsync();
        var mappedTrips = trips.Select(trip => trip.MapToGetTripDto()).ToList();
        return mappedTrips;
    }

    public async Task AddClientToTrip(int tripId, Client client, DateTime? paymentDate, CancellationToken token)
    {
        var peselAlreadyExists = await clientRepository.ClientWithPeselNumberExists(client.Pesel);
        if (peselAlreadyExists)
            throw new ClientExceptions.PeselAlreadyAssignedException();

        var trip = await tripRepository.GetTripAsync(tripId);
        if (trip == null)
            throw new TripExceptions.TripNotFoundException(tripId);

        if (trip.DateFrom < clock.UtcNow)
            throw new TripExceptions.TripAlreadyOccuredException(tripId);

        var clientTrip = new ClientTrip
        {
            IdClientNavigation = client,
            IdTripNavigation = trip,
            PaymentDate = paymentDate,
            RegisteredAt = clock.UtcNow,
        };

        await clientTripRepository.RegisterNewClientToTripAsync(clientTrip);
        await clientTripRepository.SaveChangesAsync();
    }
}