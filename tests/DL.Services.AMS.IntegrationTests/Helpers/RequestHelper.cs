using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.UseCases.Account.Create;
using DL.Services.AMS.Domain.UseCases.AccountDetails.Edit;
using System;

namespace DL.Services.AMS.IntegrationTests.Helpers
{
    public static class RequestHelper
    {
        public static CreateAccountRequest CreateAccountRequest(int id,
            Reference reference)
        {
            var random = new Random();
            return new CreateAccountRequest()
            {
                Reference = reference,
                Username = $"TestUser{id}",
                FirstName = $"FirstName{id}",
                Surname = $"Surname{id}",
                DateOfBirth = RandomHelper.GenerateDate(1900, 18),
                AddressLine1 = $"{id}TestRoad",
                AddressLine2 = $"{id}TestGrove",
                PostCode = $"{RandomHelper.GenerateString(6)}",
                Country = $"TestCountry",
                EmailAddress = $"TestEmail{id}@test.com"         
            };
        }

        public static EditAccountDetailsRequest EditAccountDetailsRequest(int accountId)
        {
            var random = new Random();
            return new EditAccountDetailsRequest()
            {
                AccountId = accountId,
                FirstName = $"UpdatedFirstName{accountId}",
                Surname = $"UpdatedSurname{accountId}",
                DateOfBirth = RandomHelper.GenerateDate(1900, 18),
                AddressLine1 = $"Updated{accountId}TestRoad",
                AddressLine2 = $"Updated{accountId}TestGrove",
                PostCode = $"{RandomHelper.GenerateString(6)}",
                Country = $"UpdatedTestCountry",
                EmailAddress = $"UpdatedTestEmail{accountId}@test.com"
            };
        }
    }
}
