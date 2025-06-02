namespace Tutorial10.Application.Exceptions;

public class InvalidDueDateException : Exception
{
    public InvalidDueDateException(DateTime date, DateTime dueDate)
        : base($"The due date {dueDate} cannot be earlier than the prescription date {date}.")
    {
    }
}
