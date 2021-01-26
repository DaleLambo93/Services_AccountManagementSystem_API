using System;
using DL.Services.AMS.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Services.AMS.Domain.UseCases
{
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UseCaseFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IUseCase<TOut, TIn> Get<TOut, TIn>()
        {
            var service = _serviceProvider.GetService<IUseCase<TOut, TIn>>();

            return service ?? throw new UseCaseNotFoundException(
                       $"UseCase<{typeof(TOut).Name},{typeof(TIn).Name}> not found.");
        }
    }
}
