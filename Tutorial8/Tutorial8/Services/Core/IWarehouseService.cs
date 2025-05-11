using Tutorial8.Contracts.Requests;

namespace Tutorial8.Services.Core;

public interface IWarehouseService
{
    Task<int> AddProductToWarehouse(ProductWarehouseRequest request);
    Task<int> AddProductToWarehouseWithProc(ProductWarehouseRequest request);
}
