namespace Tutorial7.Exceptions
{
    public class ParticipantsWillBeExceededException() : Exception("Failed to register client to a new trip. Max participants will be exceeded")
    {
    }
}
