using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Updaters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Confirm
{
    public class ConfirmAccountUseCase : IUseCase<ConfirmAccountRequest, ConfirmAccountResponse>
    {
        private readonly IAccountFetcher _fetcher;
        private readonly IAccountUpdater _updater;
        private readonly ILogger<ConfirmAccountUseCase> _logger;

        public ConfirmAccountUseCase(IAccountFetcher fetcher,
            IAccountUpdater updater,
            ILogger<ConfirmAccountUseCase> logger)
        {
            _fetcher = fetcher;
            _updater = updater;
            _logger = logger;
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
            _logger.LogInformation($"Account with Id {request.AccountId} is now Confirmed and Active.");

            return new ConfirmAccountResponse()
            {
                AccountId = accountEntity.Id,
                Status = accountEntity.Status
            };
        }
    }
}
