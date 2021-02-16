using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Removers;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Remove
{
    public class RemoveAccountUseCase : IUseCase<RemoveAccountRequest, BaseResponse>
    {
        private readonly IAccountFetcher _fetcher;
        private readonly IAccountRemover _remover;
        private readonly ILogger<RemoveAccountUseCase> _logger;

        public RemoveAccountUseCase(IAccountFetcher fetcher,
            IAccountRemover remover,
            ILogger<RemoveAccountUseCase> logger)
        {
            _fetcher = fetcher;
            _remover = remover;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(RemoveAccountRequest request)
        {
            var accountEntity = await _fetcher.Fetch(request.AccountId);

            if (accountEntity is null)
            {
                return new BaseResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Reason = $"Account with Id {request.AccountId} was not found."
                };
            }

            await _remover.Remove(accountEntity.Id);
            _logger.LogInformation($"Account with Id {request.AccountId} was removed.");

            return new BaseResponse()
            {
                Reason = $"Account with Id {accountEntity.Id} was deleted."
            };
        }
    }
}
