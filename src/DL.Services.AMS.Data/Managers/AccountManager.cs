using DL.Services.AMS.Data.Mappers;
using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Data.Repositories;
using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.Ports.Creators;
using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Removers;
using DL.Services.AMS.Domain.Ports.Updaters;
using System;
using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Managers
{
    public class AccountManager : IAccountFetcher,
        IAccountCreator, 
        IAccountUpdater,
        IAccountRemover
    {
        private readonly IAccountRepository _repository;
        private readonly IMapperFactory _mapperFactory;

        public AccountManager(IAccountRepository repository,
            IMapperFactory mapperFactory)
        {
            _repository = repository;
            _mapperFactory = mapperFactory;
        }

        public async Task<AccountEntity> Create(AccountEntity accountEntity)
        {
            var account = _mapperFactory.Get<Account, AccountEntity>()
                .Map(accountEntity);

            account = await _repository.Add(account);

            return _mapperFactory.Get<AccountEntity, Account>()
                .Map(account);
        }

        public async Task<AccountEntity> Fetch(int accountId)
        {
            var account = await _repository.Get(accountId);

            return _mapperFactory.Get<AccountEntity, Account>()
                .Map(account);
        }

        public async Task<AccountEntity> FetchWithDetails(int accountId)
        {
            var account = await _repository.GetWithDetails(accountId);

            return _mapperFactory.Get<AccountEntity, Account>()
                .Map(account);
        }

        public async Task<AccountEntity> FetchByUsername(string username)
        {
            var account = await _repository.GetByUsername(username);

            return _mapperFactory.Get<AccountEntity, Account>()
                .Map(account);
        }

        public async Task<AccountEntity> UpdateStatus(int accountId, AccountStatus status)
        {
            var account = await _repository.Get(accountId);

            account.Status = status;
            account.ModifiedAt = DateTime.Now;

            await _repository.SaveChanges();

            return _mapperFactory.Get<AccountEntity, Account>()
                .Map(account);
        }

        public async Task Remove(int accountId)
        {
            var account = await _repository.GetWithDetails(accountId);

            await _repository.Remove(account);
        }
    }
}
