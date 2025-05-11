namespace Tutorial8.Exceptions;

public class OrderAlreadyFulfilledException : Exception
{
    public OrderAlreadyFulfilledException() : base("Order already fulfilled") { }
}
