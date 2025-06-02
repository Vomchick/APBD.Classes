namespace Tutorial10.Application.Exceptions;

public class TooManyMedicamentsException : Exception
{
    public TooManyMedicamentsException(int count)
        : base($"Too many medicaments in the prescription. Maximum allowed is 10, but {count} were provided.")
    {
    }
}
