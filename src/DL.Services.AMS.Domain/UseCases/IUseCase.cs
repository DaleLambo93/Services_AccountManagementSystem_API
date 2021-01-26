using System.Threading.Tasks;

namespace DL.Services.AMS.Domain.UseCases
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }
}
