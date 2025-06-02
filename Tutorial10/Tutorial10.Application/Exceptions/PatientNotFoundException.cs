﻿namespace Tutorial10.Application.Exceptions;

public class PatientNotFoundException : Exception
{
    public PatientNotFoundException(int id)
        : base($"Patient with ID {id} not found.")
    {
    }
}
