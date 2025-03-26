using System;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IClientRepository _repository;
        private readonly IUserCreditService _userCreditService;
        private readonly IClock _clock;

        public UserService() : this(new ClientRepository(), new UserCreditService(), new Clock()) {}

        public UserService(IClientRepository repository, IUserCreditService userCreditService, IClock clock)
        {
            _repository = repository;
            _userCreditService = userCreditService;
            _clock = clock;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidInput(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            var client = _repository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (!TrySetCreditLimit(client, user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool IsValidInput(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains('@') && !email.Contains('.'))
            {
                return false;
            }

            var now = _clock.Now();
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age >= 21;
        }

        private bool TrySetCreditLimit(Client client, User user)
        {
            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                {
                    var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit *= 2;
                    user.CreditLimit = creditLimit;
                    _userCreditService.Dispose();
                    break;
                }
                default:
                {
                    user.HasCreditLimit = true;
                    var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                    _userCreditService.Dispose();
                    break;
                }
            }

            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }
    }
}
