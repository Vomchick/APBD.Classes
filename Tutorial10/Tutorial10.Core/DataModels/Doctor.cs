﻿namespace Tutorial10.Core.DataModels;

public class Doctor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
