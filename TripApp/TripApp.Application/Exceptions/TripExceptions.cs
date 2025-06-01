using TripApp.Core.Model;

namespace TripApp.Application.Exceptions;

public class TripExceptions
{
    public class TripNotFoundException(int tripId)
        : BaseExceptions.NotFoundException($"Trip with id {tripId} was not found");

    public class TripAlreadyOccuredException(int tripId)
        : Exception($"Trip with id {tripId} already occured");
}
