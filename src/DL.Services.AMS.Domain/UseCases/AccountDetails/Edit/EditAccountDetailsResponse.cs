using DL.Services.AMS.Domain.Entities;

namespace DL.Services.AMS.Domain.UseCases.AccountDetails.Edit
{
    public class EditAccountDetailsResponse : BaseResponse
    {
        public AccountDetailsEntity AccountDetailsEntity { get; set; }
    }
}
