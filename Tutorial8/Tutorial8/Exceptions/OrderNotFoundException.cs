namespace Tutorial8.Exceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException() : base("Matching order not found") { }
}