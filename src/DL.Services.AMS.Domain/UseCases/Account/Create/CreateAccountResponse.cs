using DL.Services.AMS.Domain.Entities;

namespace DL.Services.AMS.Domain.UseCases.Account.Create
{
    public class CreateAccountResponse : BaseResponse
    {
        public AccountEntity Account { get; set; }
    }
}
