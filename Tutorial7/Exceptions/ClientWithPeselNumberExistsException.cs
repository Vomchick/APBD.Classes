namespace Tutorial7.Exceptions
{
    public class ClientWithPeselNumberExistsException(string pesel) : Exception($"Client with provided PESEL {pesel} already exists")
    {
    }
}
