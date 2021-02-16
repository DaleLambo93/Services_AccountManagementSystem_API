using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Updaters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Cancel
{
    public class CancelAccountUseCase : IUseCase<CancelAccountRequest, CancelAccountResponse>
    {
        private readonly IAccountFetcher _fetcher;
        private readonly IAccountUpdater _updater;
        private readonly ILogger<CancelAccountUseCase> _logger;

        public CancelAccountUseCase(IAccountFetcher fetcher,
            IAccountUpdater updater,
            ILogger<CancelAccountUseCase> logger)
        {
            _fetcher = fetcher;
            _updater = updater;
            _logger = logger;
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
            _logger.LogInformation($"Account with Id {request.AccountId} is now Cancelled.");

            return new CancelAccountResponse()
            {
                AccountId = accountEntity.Id,
                Status = accountEntity.Status
            };
        }
    }
}
