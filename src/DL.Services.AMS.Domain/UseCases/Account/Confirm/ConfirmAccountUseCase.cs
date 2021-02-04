using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Updaters;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Confirm
{
    public class ConfirmAccountUseCase : IUseCase<ConfirmAccountRequest, ConfirmAccountResponse>
    {
        private readonly IAccountFetcher _fetcher;
        private readonly IAccountUpdater _updater;

        public ConfirmAccountUseCase(IAccountFetcher fetcher,
            IAccountUpdater updater)
        {
            _fetcher = fetcher;
            _updater = updater;
        }

        public async Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request)
        {
            var accountEntity = await _fetcher.Fetch(request.AccountId);

            if (accountEntity is null)
            {
                return new ConfirmAccountResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Reason = $"Account with Id {request.AccountId} could not be found."
                };
            }

            accountEntity = await _updater
                .UpdateStatus(accountEntity.Id, Entities.Constants.AccountStatus.Active);

            return new ConfirmAccountResponse()
            {
                AccountId = accountEntity.Id,
                Status = accountEntity.Status
            };
        }
    }
}
