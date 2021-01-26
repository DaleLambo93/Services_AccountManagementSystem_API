namespace DL.Services.AMS.Domain.UseCases
{
    public interface IUseCaseFactory
    {
        IUseCase<TResponse, TRequest> Get<TResponse, TRequest>();
    }
}
