using DL.Services.AMS.Data.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Services.AMS.IntegrationTests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryDb(this IServiceCollection services)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddEntityFrameworkSqlite()
                .AddDbContext<AMSDbContext>(options => options
                    .UseSqlite(connection));
        }
    }
}
