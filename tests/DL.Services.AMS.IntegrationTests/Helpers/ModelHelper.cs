using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.Helpers;
using System;

namespace DL.Services.AMS.IntegrationTests.Helpers
{
    public static class ModelHelper
    {
        public static Account CreateAccount(int id,
            int? detailsId,
            Reference reference,
            AccountStatus status)
        {
            return new Account()
            {
                Id = id,
                Username = $"TestName{id}",
                CustomerReference = CustomerReferenceHelper.GetCustomerReference(reference),
                Status = status,
                CreatedAt = DateTime.Now,
                AccountDetails = detailsId is null ? null : CreateAccountDetails(detailsId.Value, id)
            };
        }

        public static AccountDetails CreateAccountDetails(int id,
            int accountId)
        {
            var random = new Random();        
            return new AccountDetails()
            {
                Id = id,
                AccountId = accountId,
                FirstName = $"TestFirstName{id}",
                Surname = $"TestSurname{id}",
                DateOfBirth = RandomHelper.GenerateDate(1900, 18),
                AddressLine1 = $"TestAddress1",
                AddressLine2 = $"TestAddress2",
                PostCode = $"{RandomHelper.GenerateString(6)}",
                Country = "TestCountry",
                CreatedAt = DateTime.Now,
                EmailAddress = $"TestEmail{id}@test.com",               
            };
        }
    }
}
