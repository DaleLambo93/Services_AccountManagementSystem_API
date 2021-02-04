using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Entities.Constants;
using Microsoft.Extensions.Logging;

namespace DL.Services.AMS.Data.Mappers.EntityToModel
{
    public class AccountMapper : Mapper<Account, AccountEntity>
    {
        private readonly ILogger<Mapper<Account, AccountEntity>> _logger;
        private readonly IMapperFactory _mapperFactory;

        public AccountMapper(ILogger<Mapper<Account, AccountEntity>> logger,
            IMapperFactory mapperFactory) : base(logger)
        {
            _logger = logger;
            _mapperFactory = mapperFactory;
        }

        public override Account Map(AccountEntity item)
        {
            if (!Validate(item))
            {
                return null;
            }

            var account = new Account()
            {
                Id = item.Id,
                Username = item.Username,
                CustomerReference = item.CustomerReference,
                Status = AccountStatus.Pending,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };

            var mapper = _mapperFactory.Get<AccountDetails, AccountDetailsEntity>();

            if (item.AccountDetailsEntity != null)
            {
                account.AccountDetails = mapper.Map(item.AccountDetailsEntity);
            }
            else
            {
                _logger.LogDebug($"AccountDetailsEntity for AccountEntity Id {item.Id} is null.");
            }

            return account;
        }        
    }
}
