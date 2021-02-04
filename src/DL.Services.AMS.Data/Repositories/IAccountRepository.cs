using DL.Services.AMS.Data.Models;
using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByUsername(string username);
        Task<Account> GetWithDetails(int accountId);
    }
}
