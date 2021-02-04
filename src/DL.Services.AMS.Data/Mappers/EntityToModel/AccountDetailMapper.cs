using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DL.Services.AMS.Data.Mappers.EntityToModel
{
    public class AccountDetailMapper : Mapper<AccountDetails, AccountDetailsEntity>
    {
        public AccountDetailMapper(ILogger<Mapper<AccountDetails, AccountDetailsEntity>> logger) : base(logger)
        {
        }

        public override AccountDetails Map(AccountDetailsEntity item)
        {
            if (!Validate(item))
            {
                return null;
            }

            return new AccountDetails()
            {
                Id = item.Id,
                AccountId = item.AccountId,
                FirstName = item.FirstName,
                Surname = item.Surname,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                PostCode = item.PostCode,
                Country = item.Country,
                DateOfBirth = item.DateOfBirth,
                EmailAddress = item.EmailAddress,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt
            };
        }
    }
}
