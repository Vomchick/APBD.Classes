using System;

namespace LegacyApp.Interfaces
{
    public interface IUserCreditService : IDisposable
    {
        int GetCreditLimit(string lastName, DateTime dateOfBirth);
    }
}
