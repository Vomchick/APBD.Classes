namespace Tutorial10.Application.Exceptions;

public class DoctorNotFoundException : Exception
{
    public DoctorNotFoundException(int id)
        : base($"Doctor with ID {id} not found.")
    {
    }
}
