using DL.Services.AMS.Domain.Entities.Constants;

namespace DL.Services.AMS.Domain.Entities
{
    public class AccountEntity : EntityBase
    {
        public string Username { get; set; }
        public string CustomerReference { get; set; }
        public AccountStatus Status { get; set; }
        public AccountDetailsEntity AccountDetailsEntity { get; set; }
    }
}
