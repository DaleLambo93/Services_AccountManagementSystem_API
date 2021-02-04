using DL.Services.AMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DL.Services.AMS.Data.Context
{
    public class AMSDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountDetails> AccountDetails { get; set; }

        public AMSDbContext()
        {
        }

        public AMSDbContext(DbContextOptions<AMSDbContext> options)
             : base(options)
        {
        }
    }
}
