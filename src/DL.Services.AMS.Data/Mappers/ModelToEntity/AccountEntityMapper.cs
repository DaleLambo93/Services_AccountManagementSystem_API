using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DL.Services.AMS.Data.Mappers.ModelToEntity
{
    public class AccountEntityMapper : Mapper<AccountEntity, Account>
    {
        private readonly ILogger<Mapper<AccountEntity, Account>> _logger;
        private readonly IMapperFactory _mapperFactory;

        public AccountEntityMapper(ILogger<Mapper<AccountEntity, Account>> logger,
            IMapperFactory mapperFactory) : base(logger)
        {
            _logger = logger;
            _mapperFactory = mapperFactory;
        }

        public override AccountEntity Map(Account item)
        {
            if (!Validate(item))
            {
                return null;
            }

            var accountEntity = new AccountEntity()
            {
                Id = item.Id,
                Username = item.Username,
                CustomerReference = item.CustomerReference,
                Status = item.Status,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };

            var mapper = _mapperFactory.Get<AccountDetailsEntity, AccountDetails>();

            if (item.AccountDetails != null)
            {
                accountEntity.AccountDetailsEntity = mapper.Map(item.AccountDetails);
            }
            else
            {
                _logger.LogDebug($"AccountDetails for Account Id {item.Id} is null.");
            }

            return accountEntity;
        }
    }
}
