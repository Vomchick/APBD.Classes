namespace Tutorial8.Exceptions;

public class WarehouseNotFoundException : Exception
{
    public WarehouseNotFoundException() : base("Warehouse not found") { }
}
