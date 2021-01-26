using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Entities.Constants;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.Ports.Updaters
{
    public interface IAccountUpdater
    {
        Task<AccountEntity> UpdateStatus(int accountId, AccountStatus status);
    }
}
