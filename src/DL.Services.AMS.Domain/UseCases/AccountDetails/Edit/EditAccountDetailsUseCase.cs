using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Updaters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.AccountDetails.Edit
{
    public class EditAccountDetailsUseCase : IUseCase<EditAccountDetailsRequest, EditAccountDetailsResponse>
    {
        private readonly IAccountDetailsFetcher _fetcher;
        private readonly IAccountDetailsUpdater _updater;
        private readonly ILogger<EditAccountDetailsUseCase> _logger;

        public EditAccountDetailsUseCase(IAccountDetailsFetcher fetcher,
            IAccountDetailsUpdater updater,
            ILogger<EditAccountDetailsUseCase> logger)
        {
            _fetcher = fetcher;
            _updater = updater;
            _logger = logger;
        }

        public async Task<EditAccountDetailsResponse> Handle(EditAccountDetailsRequest request)
        {
            var accountDetailsEntity = await _fetcher.Fetch(request.AccountId);

            if (accountDetailsEntity is null)
            {
                return new EditAccountDetailsResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Reason = $"Details for account id {request.AccountId} was not found."
                };
            }

            accountDetailsEntity = await _updater.Update(request);
            _logger.LogInformation($"Account Details with Id {request.AccountId} was updated.");

            return new EditAccountDetailsResponse()
            {
                AccountDetailsEntity = accountDetailsEntity
            };
        }
    }
}
