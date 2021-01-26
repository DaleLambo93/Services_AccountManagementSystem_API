using DL.Services.AMS.Data.Context;
using DL.Services.AMS.Data.Models;

namespace DL.Services.AMS.IntegrationTests.Helpers
{
    public static class DbContextHelper
    {
        public static void AddAccount(this AMSDbContext context,
            Account account)
        {
            context.Accounts.Add(account);
            context.SaveChanges();
        }
    }
}
