using DL.Services.AMS.Data.Context;
using DL.Services.AMS.Data.Managers;
using DL.Services.AMS.Data.Mappers;
using DL.Services.AMS.Data.Mappers.EntityToModel;
using DL.Services.AMS.Data.Mappers.ModelToEntity;
using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Data.Repositories;
using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Ports.Creators;
using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Removers;
using DL.Services.AMS.Domain.Ports.Updaters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DL.Services.AMS.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddMapperServices();
            services.AddManagerServices();
            services.AddRepositoryServices();

            services.AddDbContext<AMSDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                        builder => builder.EnableRetryOnFailure(6, TimeSpan.FromSeconds(10), null)),
                        ServiceLifetime.Transient);
        }

        private static void AddMapperServices(this IServiceCollection services)
        {
            services.AddTransient<IMapperFactory, MapperFactory>();
            services.AddTransient<IMapper<Account, AccountEntity>, AccountMapper>();
            services.AddTransient<IMapper<AccountDetails, AccountDetailsEntity>, AccountDetailMapper>();
            services.AddTransient<IMapper<AccountEntity, Account>, AccountEntityMapper>();
            services.AddTransient<IMapper<AccountDetailsEntity, AccountDetails>, AccountDetailEntityMapper>();
        }

        private static void AddManagerServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountFetcher, AccountManager>();
            services.AddTransient<IAccountCreator, AccountManager>();
            services.AddTransient<IAccountUpdater, AccountManager>();
            services.AddTransient<IAccountRemover, AccountManager>();

            services.AddTransient<IAccountDetailsFetcher, AccountDetailsManager>();
            services.AddTransient<IAccountDetailsUpdater, AccountDetailsManager>();
        }

        private static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountDetailsRepository, AccountDetailsRepository>();
        }
    }
}
