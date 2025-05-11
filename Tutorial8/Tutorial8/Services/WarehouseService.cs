using Microsoft.Data.SqlClient;
using System.Data;
using Tutorial8.Contracts.Requests;
using Tutorial8.Exceptions;
using Tutorial8.Services.Core;

namespace Tutorial8.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IConfiguration _configuration;

    public WarehouseService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> AddProductToWarehouse(ProductWarehouseRequest request)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        using var transaction = connection.BeginTransaction();

        try
        {
            var productExistsCmd = new SqlCommand("SELECT 1 FROM Product WHERE IdProduct = @Id", connection, transaction);
            productExistsCmd.Parameters.AddWithValue("@Id", request.IdProduct);
            if ((await productExistsCmd.ExecuteScalarAsync()) == null)
                throw new ProductNotFoundException();

            var warehouseExistsCmd = new SqlCommand("SELECT 1 FROM Warehouse WHERE IdWarehouse = @Id", connection, transaction);
            warehouseExistsCmd.Parameters.AddWithValue("@Id", request.IdWarehouse);
            if ((await warehouseExistsCmd.ExecuteScalarAsync()) == null)
                throw new WarehouseNotFoundException();

            if (request.Amount <= 0)
                throw new InvalidAmountException();

            var orderCmd = new SqlCommand(@"
                SELECT TOP 1 * FROM [Order]
                WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt
            ", connection, transaction);
            orderCmd.Parameters.AddWithValue("@IdProduct", request.IdProduct);
            orderCmd.Parameters.AddWithValue("@Amount", request.Amount);
            orderCmd.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

            int orderId = -1;
            using (var reader = await orderCmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                    orderId = (int)reader["IdOrder"];
                else
                    throw new OrderNotFoundException();
            }

            var checkCmd = new SqlCommand("SELECT 1 FROM Product_Warehouse WHERE IdOrder = @IdOrder", connection, transaction);
            checkCmd.Parameters.AddWithValue("@IdOrder", orderId);
            if ((await checkCmd.ExecuteScalarAsync()) != null)
                throw new OrderAlreadyFulfilledException();

            var updateCmd = new SqlCommand("UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder", connection, transaction);
            updateCmd.Parameters.AddWithValue("@IdOrder", orderId);
            await updateCmd.ExecuteNonQueryAsync();

            var priceCmd = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @IdProduct", connection, transaction);
            priceCmd.Parameters.AddWithValue("@IdProduct", request.IdProduct);
            var price = Convert.ToDecimal(await priceCmd.ExecuteScalarAsync());

            var insertCmd = new SqlCommand(@"
                INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                OUTPUT INSERTED.IdProductWarehouse
                VALUES (@Warehouse, @Product, @Order, @Amount, @Price, GETDATE())
            ", connection, transaction);
            insertCmd.Parameters.AddWithValue("@Warehouse", request.IdWarehouse);
            insertCmd.Parameters.AddWithValue("@Product", request.IdProduct);
            insertCmd.Parameters.AddWithValue("@Order", orderId);
            insertCmd.Parameters.AddWithValue("@Amount", request.Amount);
            insertCmd.Parameters.AddWithValue("@Price", price * request.Amount);

            int insertedId = (int)await insertCmd.ExecuteScalarAsync();
            transaction.Commit();
            return insertedId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<int> AddProductToWarehouseWithProc(ProductWarehouseRequest request)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var command = new SqlCommand("AddProductToWarehouse", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@IdProduct", request.IdProduct);
        command.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
        command.Parameters.AddWithValue("@Amount", request.Amount);
        command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

        try
        {
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            throw new Exception("Stored procedure execution failed: " + ex.Message);
        }
    }
}

