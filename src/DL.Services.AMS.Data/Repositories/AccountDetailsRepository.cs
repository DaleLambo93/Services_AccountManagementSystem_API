using DL.Services.AMS.Data.Context;
using DL.Services.AMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Repositories
{
    public class AccountDetailsRepository : Repository<AccountDetails>, IAccountDetailsRepository
    {
        private readonly AMSDbContext _context;

        public AccountDetailsRepository(AMSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AccountDetails> GetByAccountId(int accountId)
        {
            return await _context.AccountDetails
                .FirstOrDefaultAsync(x => x.AccountId == accountId);
        }
    }
}
