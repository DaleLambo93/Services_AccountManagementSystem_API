using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.UseCases.AccountDetails.Edit;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.Ports.Updaters
{
    public interface IAccountDetailsUpdater
    {
        Task<AccountDetailsEntity> Update(EditAccountDetailsRequest request);
    }
}
