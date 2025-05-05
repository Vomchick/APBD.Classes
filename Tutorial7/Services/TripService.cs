using Microsoft.Data.SqlClient;
using Tutorial7.Entities;
using Tutorial7.Exceptions;
using Tutorial7.Services.Core;

namespace Tutorial7.Services;

public class TripService : ITripService
{
    private readonly string _connectionString;
    private readonly IClientService _clientService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TripService(IConfiguration cfg, IClientService clientService, IDateTimeProvider dateTimeProvider)
    {
        _clientService = clientService;
        _dateTimeProvider = dateTimeProvider;
        _connectionString = cfg.GetConnectionString("DefaultConnection") ??
                            throw new ArgumentNullException(nameof(cfg),
                                "Default connection string configuration is missing");
    }

    public async Task<ICollection<Trip>> GetAllTripsWithCountriesAsync(CancellationToken token = default)
    {
        Dictionary<int, Trip> trips = new();
        const string query = """
                         SELECT t.IdTrip,
                                t.Name,
                                t.Description,
                                t.DateFrom,
                                t.DateTo,
                                t.MaxPeople,
                                c.IdCountry,
                                c.Name
                         FROM Country_Trip as ct 
                             LEFT JOIN Country as c on ct.IdCountry = c.IdCountry 
                             LEFT JOIN Trip as t on ct.IdTrip = t.IdTrip
                         """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand cmd = new(query, con);
        await con.OpenAsync(token);
        await using var reader = await cmd.ExecuteReaderAsync(token);

        while (await reader.ReadAsync(token))
        {
            var tripId = reader.GetInt32(0);

            if (!trips.TryGetValue(tripId, out var trip))
            {
                trip = new Trip
                {
                    Id = tripId,
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    DateFrom = reader.GetDateTime(3),
                    DateTo = reader.GetDateTime(4),
                    MaxPeople = reader.GetInt32(5),
                    Countries = []
                };
                trips.Add(tripId, trip);
            }

            if (!reader.IsDBNull(6))
            {
                var countryId = reader.GetInt32(6);
                var countryExists = trip.Countries?.Any(c => c.Id == countryId) ?? false;
                if (!countryExists)
                {
                    Country country = new()
                    {
                        Id = countryId,
                        Name = reader.GetString(7)
                    };

                    trip.Countries!.Add(country);
                }
            }
        }

        return trips.Values.ToList();
    }

    public async ValueTask<bool> UpdateClientTripAsync(int clientId, int tripId, CancellationToken token = default)
    {
        // Keeping in mind that controlling the flow using the exceptions like here is not the best approach :)
        // Theoretically this can be wrapped as a transaction, but who cares :)
        await ValidateTripExistsAsync(tripId, token);
        await ValidateClientExistsAsync(clientId, token);

        await using SqlConnection con = new(_connectionString);
        await con.OpenAsync(token);

        var totalParticipantsQuery = """
                                    SELECT COUNT(*)
                                    FROM Client_Trip as ct
                                    WHERE ct.IdTrip = @tripId;
                                 """;

        await using SqlCommand totalParticipantsCommand = new(totalParticipantsQuery, con);
        totalParticipantsCommand.Parameters.AddWithValue("@tripId", tripId);
        var totalParticipants = Convert.ToInt32(await totalParticipantsCommand.ExecuteScalarAsync(token));

        var maxParticipantsQuery = """
                                 SELECT t.MaxPeople
                                 FROM Trip as t
                                 WHERE t.IdTrip = @tripId;
                               """;

        await using SqlCommand maxParticipantsCommand = new(maxParticipantsQuery, con);
        maxParticipantsCommand.Parameters.AddWithValue("@tripId", tripId);
        var maxParticipants = Convert.ToInt32(await maxParticipantsCommand.ExecuteScalarAsync(token));

        if (totalParticipants + 1 > maxParticipants)
            throw new ParticipantsWillBeExceededException();

        var addClientTripQuery = """
                                INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate)
                                VALUES (@idClient, @idTrip, @registeredAt, @paymentDate)
                                """;

        await using var addClientTripCommand = new SqlCommand(addClientTripQuery, con);
        addClientTripCommand.Parameters.AddWithValue("@idClient", clientId);
        addClientTripCommand.Parameters.AddWithValue("@idTrip", tripId);
        addClientTripCommand.Parameters.AddWithValue("@registeredAt", Convert.ToInt32(_dateTimeProvider.UtcNow.ToString("yyyyMMdd")));
        addClientTripCommand.Parameters.AddWithValue("@paymentDate", 0); //It will break at reading if value is DBNull.Value

        var rowsAffected = Convert.ToInt32(await addClientTripCommand.ExecuteNonQueryAsync(token));
        return rowsAffected > 0;
    }

    public async ValueTask<bool> TripExistsByTripId(int tripId, CancellationToken token = default)
    {
        if (tripId <= 0)
            return false;

        const string query = """
                         SELECT 
                             IIF(EXISTS (SELECT 1 FROM Trip 
                                     WHERE Trip.IdTrip = @tripId), 1, 0) AS TripExists;   
                         """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand cmd = new(query, con);
        await con.OpenAsync(token);
        cmd.Parameters.AddWithValue("@tripId", tripId);

        var result = (int)await cmd.ExecuteScalarAsync(token);
        return result == 1;
    }

    private async Task ValidateTripExistsAsync(int tripId, CancellationToken token = default)
    {
        var tripExists = await TripExistsByTripId(tripId, token);
        if (!tripExists)
            throw new TripDoesNotExistException(tripId);
    }

    private async Task ValidateClientExistsAsync(int clientId, CancellationToken token = default)
    {
        var clientExists = await _clientService.ClientExistsByIdAsync(clientId, token);
        if (!clientExists)
            throw new ClientDoesNotExistException(clientId);
    }

    public async Task<bool> DeleteClientTripAsync(int clientId, int tripId, CancellationToken token)
    {
        const string checkQuery = """
                                    SELECT COUNT(*)
                                    FROM Client_Trip
                                    WHERE IdClient = @clientId AND IdTrip = @tripId;
                                """;

        await using SqlConnection con = new(_connectionString);
        await con.OpenAsync(token);

        await using SqlCommand checkCmd = new(checkQuery, con);
        checkCmd.Parameters.AddWithValue("@clientId", clientId);
        checkCmd.Parameters.AddWithValue("@tripId", tripId);

        var exists = Convert.ToInt32(await checkCmd.ExecuteScalarAsync(token)) > 0;

        if (!exists)
        {
            return false;
        }

        const string deleteQuery = """
                                    DELETE FROM Client_Trip
                                    WHERE IdClient = @clientId AND IdTrip = @tripId;
                                """;

        await using SqlCommand deleteCmd = new(deleteQuery, con);
        deleteCmd.Parameters.AddWithValue("@clientId", clientId);
        deleteCmd.Parameters.AddWithValue("@tripId", tripId);

        var rowsAffected = await deleteCmd.ExecuteNonQueryAsync(token);
        return rowsAffected > 0;
    }
}
