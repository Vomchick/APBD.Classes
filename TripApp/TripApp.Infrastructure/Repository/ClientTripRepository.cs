using TripApp.Application.Repository;
using TripApp.Core.Model;

namespace Trip.Infrastructure.Repository;

public class ClientTripRepository(TripContext context) : IClientTripRepository
{
    public async Task RegisterNewClientToTripAsync(ClientTrip clientTrip)
    {
        await context.ClientTrips.AddAsync(clientTrip);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
