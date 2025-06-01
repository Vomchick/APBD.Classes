using TripApp.Core.Model;

namespace TripApp.Application.Repository;

public interface IClientTripRepository
{
    Task RegisterNewClientToTripAsync(ClientTrip clientTrip);
    Task SaveChangesAsync();
}
