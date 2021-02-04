using DL.Services.AMS.Data.Models;
using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Repositories
{
    public interface IAccountDetailsRepository : IRepository<AccountDetails>
    {
        Task<AccountDetails> GetByAccountId(int accountId);
    }
}
