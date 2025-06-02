namespace Tutorial10.Application.Exceptions;

public class MedicationNotFoundException : Exception
{
    public MedicationNotFoundException(int id)
        : base($"Medication with ID {id} not found.")
    {
    }
}
