using Tutorial7.Entities;

namespace Tutorial7.Services.Core;

public interface ITripService
{
    public Task<ICollection<Trip>> GetAllTripsWithCountriesAsync(CancellationToken token = default);
    public ValueTask<bool> TripExistsByTripId(int tripId, CancellationToken token = default);
    public ValueTask<bool> UpdateClientTripAsync(int clientId, int tripId, CancellationToken token = default);
}
