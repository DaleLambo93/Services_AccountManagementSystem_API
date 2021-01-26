using DL.Services.AMS.Domain.Ports.Fetchers;
using DL.Services.AMS.Domain.Ports.Removers;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases.Account.Remove
{
    public class RemoveAccountUseCase : IUseCase<RemoveAccountRequest, BaseResponse>
    {
        private readonly IAccountFetcher _fetcher;
        private readonly IAccountRemover _remover;

        public RemoveAccountUseCase(IAccountFetcher fetcher,
            IAccountRemover remover)
        {
            _fetcher = fetcher;
            _remover = remover;
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

            return new BaseResponse()
            {
                Reason = $"Account with Id {accountEntity.Id} was deleted."
            };
        }
    }
}
