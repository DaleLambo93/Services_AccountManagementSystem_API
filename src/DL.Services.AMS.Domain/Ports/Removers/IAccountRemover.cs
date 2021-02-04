using DL.Services.AMS.Domain.Entities;
using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.Ports.Removers
{
    public interface IAccountRemover
    {
        Task Remove(int accountId);
    }
}
