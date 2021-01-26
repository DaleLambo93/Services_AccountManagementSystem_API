using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Updaters;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.AccountDetails.Edit
{
    public class EditAccountDetailsUseCase : IUseCase<EditAccountDetailsRequest, EditAccountDetailsResponse>
    {
        private readonly IAccountDetailsFetcher _fetcher;
        private readonly IAccountDetailsUpdater _updater;

        public EditAccountDetailsUseCase(IAccountDetailsFetcher fetcher,
            IAccountDetailsUpdater updater)
        {
            _fetcher = fetcher;
            _updater = updater;
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

            return new EditAccountDetailsResponse()
            {
                AccountDetailsEntity = accountDetailsEntity
            };
        }
    }
}
