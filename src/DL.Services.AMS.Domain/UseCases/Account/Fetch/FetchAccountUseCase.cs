using DL.Services.AMS.Domain.Ports.Fetchers;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Fetch
{
    public class FetchAccountUseCase : IUseCase<FetchAccountRequest, FetchAccountResponse>
    {
        private readonly IAccountFetcher _fetcher;
        private readonly ILogger<FetchAccountUseCase> _logger;

        public FetchAccountUseCase(IAccountFetcher fetcher,
            ILogger<FetchAccountUseCase> logger)
        {
            _fetcher = fetcher;
            _logger = logger;
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
            _logger.LogInformation($"Account with Id {request.AccountId} was retrieved.");

            return new FetchAccountResponse()
            {
                AccountEntity = accountEntity
            };
        }
    }
}
