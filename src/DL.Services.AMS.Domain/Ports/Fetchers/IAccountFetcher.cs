using DL.Services.AMS.Domain.Entities;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.Ports.Fetchers
{
    public interface IAccountFetcher
    {
        Task<AccountEntity> Fetch(int accountId);
        Task<AccountEntity> FetchWithDetails(int accountId);
        Task<AccountEntity> FetchByUsername(string username);
    }
}
