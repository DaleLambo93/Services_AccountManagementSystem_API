using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DL.Services.AMS.Data.Mappers.ModelToEntity
{
    public class AccountDetailEntityMapper : Mapper<AccountDetailsEntity, AccountDetails>
    {
        private readonly ILogger<Mapper<AccountDetailsEntity, AccountDetails>> _logger;

        public AccountDetailEntityMapper(ILogger<Mapper<AccountDetailsEntity, AccountDetails>> logger) : base(logger)
        {
            _logger = logger;
        }

        public override AccountDetailsEntity Map(AccountDetails item)
        {
            if (!Validate(item))
            {
                return null;
            }

            return new AccountDetailsEntity()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                Surname = item.Surname,
                DateOfBirth = item.DateOfBirth,
                AccountId = item.AccountId,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                PostCode = item.PostCode,
                Country = item.Country,
                EmailAddress = item.EmailAddress,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };
        }
    }
}
