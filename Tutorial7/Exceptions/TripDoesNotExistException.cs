namespace Tutorial7.Exceptions
{
    public class TripDoesNotExistException(int tripId) : Exception($"Trip with ID {tripId} does not exist")
    {
    }
}
