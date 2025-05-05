using Microsoft.Data.SqlClient;
using System.Data;
using Tutorial7.Contracts.Requests;
using Tutorial7.Entities;
using Tutorial7.Exceptions;
using Tutorial7.Services.Core;

namespace Tutorial7.Services;

public class ClientService : IClientService
{
    private readonly string _connectionString;

    public ClientService(IConfiguration cfg)
    {
        _connectionString = cfg.GetConnectionString("DefaultConnection") ??
                            throw new ArgumentNullException(nameof(cfg), "Missing Default connection string");
    }

    public async Task<ICollection<ClientTrip>> GetAllClientTripsAsync(int clientId, CancellationToken token = default)
    {
        // Keeping in mind that controlling the flow using the exceptions like here is not the best approach :)
        await ValidateClientExistsAsync(clientId, token);

        Dictionary<int, ClientTrip> clientTrips = new();
        const string query = """
                             SELECT
                                 t.IdTrip AS TripId,
                                 t.Name AS TripName,
                                 t.Description AS TripDescription,
                                 t.DateFrom AS StartDate,
                                 t.DateTo AS EndDate,
                                 t.MaxPeople AS MaximumParticipants,
                                 ct.RegisteredAt AS RegistrationDate,
                                 ct.PaymentDate AS DateOfPayment,
                                 co.IdCountry AS CountryId,
                                 co.Name AS CountryName
                             FROM
                                 Client_Trip AS ct
                                     JOIN Trip AS t ON ct.IdTrip = t.IdTrip
                                     JOIN Country_Trip AS ctr ON ctr.IdTrip = t.IdTrip
                                     JOIN Country AS co ON co.IdCountry = ctr.IdCountry
                             WHERE
                                 ct.IdClient = @clientId;
                             """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand cmd = new(query, con);
        await con.OpenAsync(token);
        cmd.Parameters.AddWithValue("@clientId", clientId);

        await using var reader = await cmd.ExecuteReaderAsync(token);
        while (await reader.ReadAsync(token))
        {
            var tripId = reader.GetInt32(0);

            if (!clientTrips.TryGetValue(tripId, out var clientTrip))
            {
                clientTrip = new ClientTrip
                {
                    IdClient = clientId,
                    IdTrip = tripId,
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    DateFrom = reader.GetDateTime(3),
                    DateTo = reader.GetDateTime(4),
                    MaxPeople = reader.GetInt32(5),
                    RegisteredAt = reader.GetInt32(6),
                    PaymentDate = reader.GetInt32(7),
                    Countries = []
                };
                clientTrips.Add(tripId, clientTrip);
            }

            if (!reader.IsDBNull(8))
            {
                var countryId = reader.GetInt32(8);
                var countryExists = clientTrip.Countries?.Any(c => c.Id == countryId) ?? false;
                if (!countryExists)
                {
                    Country country = new()
                    {
                        Id = countryId,
                        Name = reader.GetString(9)
                    };

                    clientTrip.Countries!.Add(country);
                }
            }
        }

        return clientTrips.Values.ToList();
    }

    public async Task<int> CreateClientAsync(CreateClientRequest client, CancellationToken token = default)
    {
        // Keeping in mind that controlling the flow using the exceptions like here is not the best approach :)
        await ValidateClientExistsAsync(client.Pesel, token);

        const string query = """
                             INSERT INTO Client(FirstName, LastName, Email, Telephone, Pesel)
                             VALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel)
                             SELECT SCOPE_IDENTITY();
                             """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand cmd = new(query, con);
        await con.OpenAsync(token);
        cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
        cmd.Parameters.AddWithValue("@LastName", client.LastName);
        cmd.Parameters.AddWithValue("@Email", client.Email);
        cmd.Parameters.AddWithValue("@Telephone", client.Telephone);
        cmd.Parameters.AddWithValue("@Pesel", client.Pesel);

        var result = await cmd.ExecuteNonQueryAsync(token);
        return Convert.ToInt32(result);
    }

    public async ValueTask<bool> ClientExistsByIdAsync(int clientId, CancellationToken token = default)
    {
        if (clientId <= 0)
            return false;

        const string query = """
                             SELECT 
                                 IIF(EXISTS (SELECT 1 FROM Client 
                                         WHERE Client.IdClient = @clientId), 1, 0) AS ClientExists;   
                             """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand cmd = new(query, con);
        await con.OpenAsync(token);
        cmd.Parameters.AddWithValue("@clientId", clientId);

        var result = await cmd.ExecuteScalarAsync(token);
        return Convert.ToInt32(result) == 1;
    }

    public async ValueTask<bool> ClientExistsByPeselAsync(string pesel, CancellationToken token = default)
    {
        if (string.IsNullOrWhiteSpace(pesel))
            return false;

        const string query = """
                             SELECT 
                                 IIF(EXISTS (SELECT 1 FROM Client 
                                         WHERE Client.Pesel = @pesel), 1, 0) AS ClientExists;   
                             """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand cmd = new(query, con);
        await con.OpenAsync(token);
        cmd.Parameters.AddWithValue("@pesel", pesel);

        var result = (int)await cmd.ExecuteScalarAsync(token);
        return result == 1;
    }

    private async Task ValidateClientExistsAsync(int clientId, CancellationToken token = default)
    {
        var clientExists = await ClientExistsByIdAsync(clientId, token);
        if (!clientExists)
            throw new ClientDoesNotExistException(clientId);
    }

    private async Task ValidateClientExistsAsync(string pesel, CancellationToken token = default)
    {
        var clientExists = await ClientExistsByPeselAsync(pesel, token);
        if (clientExists)
            throw new ClientWithPeselNumberExistsException(pesel);
    }
}
