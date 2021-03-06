﻿using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.Helpers;
using DL.Services.AMS.Domain.Ports.Creators;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Create
{
    public class CreateAccountUseCase : IUseCase<CreateAccountRequest, CreateAccountResponse>
    {
        private readonly IAccountCreator _creator;
        private readonly ILogger<CreateAccountUseCase> _logger;

        public CreateAccountUseCase(IAccountCreator creator,
            ILogger<CreateAccountUseCase> logger)
        {
            _creator = creator;
            _logger = logger;
        }

        public async Task<CreateAccountResponse> Handle(CreateAccountRequest request)
        {            
            var accountEntity = new AccountEntity()
            {
                Username = request.Username,
                CustomerReference = CustomerReferenceHelper.GetCustomerReference(request.Reference),
                Status = AccountStatus.Pending,
                CreatedAt = DateTime.Now,
                AccountDetailsEntity = new AccountDetailsEntity()
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    PostCode = request.PostCode,
                    Country = request.Country,
                    DateOfBirth = request.DateOfBirth,
                    EmailAddress = request.EmailAddress,
                    CreatedAt = DateTime.Now
                }
            };

            accountEntity = await _creator.Create(accountEntity);
            _logger.LogInformation($"Account created with Id {accountEntity.Id}.");

            return new CreateAccountResponse()
            { 
                AccountId = accountEntity.Id,
                Status = accountEntity.Status
            };
        }
    }
}
