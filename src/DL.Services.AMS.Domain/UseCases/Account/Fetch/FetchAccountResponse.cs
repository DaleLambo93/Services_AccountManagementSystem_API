using DL.Services.AMS.Domain.Entities;

namespace DL.Services.AMS.Domain.UseCases.Account.Fetch
{
    public class FetchAccountResponse : BaseResponse
    {
        public AccountEntity AccountEntity { get; set; }
    }
}
