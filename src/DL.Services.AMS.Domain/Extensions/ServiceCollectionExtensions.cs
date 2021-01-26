using DL.Services.AMS.Domain.UseCases;
using DL.Services.AMS.Domain.UseCases.Account.Cancel;
using DL.Services.AMS.Domain.UseCases.Account.Confirm;
using DL.Services.AMS.Domain.UseCases.Account.Create;
using DL.Services.AMS.Domain.UseCases.Account.Fetch;
using DL.Services.AMS.Domain.UseCases.Account.Remove;
using DL.Services.AMS.Domain.UseCases.AccountDetails.Edit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Services.AMS.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddUseCaseServices();
        }

        private static void AddUseCaseServices(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseFactory, UseCaseFactory>();
            services.AddTransient<IUseCase<CreateAccountRequest, CreateAccountResponse>,
                CreateAccountUseCase>();
            services.AddTransient<IUseCase<ConfirmAccountRequest, ConfirmAccountResponse>,
                ConfirmAccountUseCase>();
            services.AddTransient<IUseCase<CancelAccountRequest, CancelAccountResponse>,
                CancelAccountUseCase>();
            services.AddTransient<IUseCase<FetchAccountRequest, FetchAccountResponse>,
                FetchAccountUseCase>();
            services.AddTransient<IUseCase<RemoveAccountRequest, BaseResponse>,
                RemoveAccountUseCase>();

            services.AddTransient<IUseCase<EditAccountDetailsRequest, EditAccountDetailsResponse>,
                EditAccountDetailsUseCase>();
        }
    }
}
