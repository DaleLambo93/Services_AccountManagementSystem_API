using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.UseCases.Account.Create;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.Ports.Creators
{
    public interface IAccountCreator
    {
        Task<AccountEntity> Create(AccountEntity accountEntity);
    }
}
