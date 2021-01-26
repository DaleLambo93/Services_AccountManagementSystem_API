using DL.Services.AMS.Domain.Entities;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.Ports.Fetchers
{
    public interface IAccountDetailsFetcher
    {
        Task<AccountDetailsEntity> Fetch(int accountId);
    }
}
