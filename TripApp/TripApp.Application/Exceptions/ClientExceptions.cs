namespace TripApp.Application.Exceptions;

public static class ClientExceptions
{
    public class ClientHasTripsException() 
        : InvalidOperationException("Client has trips.");
    
    public class ClientNotFoundException(string clientId) 
        : BaseExceptions.NotFoundException($"Client not found with client id {clientId}");

    public class PeselAlreadyAssignedException()
        : Exception("Client with such pesel number already exists");
}