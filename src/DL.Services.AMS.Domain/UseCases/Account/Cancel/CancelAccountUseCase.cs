using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Updaters;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Cancel
{
    public class CancelAccountUseCase : IUseCase<CancelAccountRequest, CancelAccountResponse>
    {
        private readonly IAccountFetcher _fetcher;
        private readonly IAccountUpdater _updater;

        public CancelAccountUseCase(IAccountFetcher fetcher,
            IAccountUpdater updater)
        {
            _fetcher = fetcher;
            _updater = updater;
        }

        public async Task<CancelAccountResponse> Handle(CancelAccountRequest request)
        {
            var accountEntity = await _fetcher.Fetch(request.AccountId);

            if (accountEntity is null)
            {
                return new CancelAccountResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Reason = $"Account with Id {request.AccountId} could not be found."
                };
            }

            accountEntity = await _updater
                .UpdateStatus(accountEntity.Id, Entities.Constants.AccountStatus.Cancelled);

            return new CancelAccountResponse()
            {
                AccountId = accountEntity.Id,
                Status = accountEntity.Status
            };
        }
    }
}
