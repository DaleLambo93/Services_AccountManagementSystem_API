using DL.Services.AMS.Data.Context;
using DL.Services.AMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DL.Services.AMS.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly AMSDbContext _context;

        public AccountRepository(AMSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account> GetByUsername(string username)
        {
            return await _context.Accounts
                .Include(x => x.AccountDetails)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<Account> GetWithDetails(int accountId)
        {
            return await _context.Accounts
                .Include(x => x.AccountDetails)
                .FirstOrDefaultAsync(x => x.Id == accountId);
        }
    }
}
