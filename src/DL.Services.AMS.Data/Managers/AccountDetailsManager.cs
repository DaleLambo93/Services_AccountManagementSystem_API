using DL.Services.AMS.Data.Mappers;
using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Data.Repositories;
using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Updaters;
using DL.Services.AMS.Domain.UseCases.AccountDetails.Edit;
using System;
using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Managers
{
    public class AccountDetailsManager : IAccountDetailsFetcher,
        IAccountDetailsUpdater
    {
        private readonly IAccountDetailsRepository _repository;
        private readonly IMapperFactory _mapperFactory;

        public AccountDetailsManager(IAccountDetailsRepository repository,
            IMapperFactory mapperFactory)
        {
            _repository = repository;
            _mapperFactory = mapperFactory;
        }

        public async Task<AccountDetailsEntity> Fetch(int accountId)
        {
            var account = await _repository.GetByAccountId(accountId);

            return _mapperFactory.Get<AccountDetailsEntity, AccountDetails>()
                .Map(account);
        }

        public async Task<AccountDetailsEntity> Update(EditAccountDetailsRequest request)
        {
            var accountDetails = await _repository
                .GetByAccountId(request.AccountId);

            accountDetails.FirstName = request.FirstName;
            accountDetails.Surname = request.Surname;
            accountDetails.DateOfBirth = request.DateOfBirth;
            accountDetails.AddressLine1 = request.AddressLine1;
            accountDetails.AddressLine2 = request.AddressLine2;
            accountDetails.Country = request.Country;
            accountDetails.PostCode = request.PostCode;
            accountDetails.EmailAddress = request.EmailAddress;
            accountDetails.ModifiedAt = DateTime.Now;

            await _repository.SaveChanges();

            return _mapperFactory.Get<AccountDetailsEntity, AccountDetails>()
                .Map(accountDetails);
        }
    }
}
