using DL.Services.AMS.Domain.Ports.Fetchers;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Fetch
{
    public class FetchAccountUseCase : IUseCase<FetchAccountRequest, FetchAccountResponse>
    {
        private readonly IAccountFetcher _fetcher;

        public FetchAccountUseCase(IAccountFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        public async Task<FetchAccountResponse> Handle(FetchAccountRequest request)
        {
            var accountEntity = await _fetcher.FetchWithDetails(request.AccountId);

            if (accountEntity is null)
            {
                return new FetchAccountResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Reason = $"Account with Id {request.AccountId} was not found."
                };
            }

            return new FetchAccountResponse()
            {
                AccountEntity = accountEntity
            };
        }
    }
}
