using System;
using DL.Services.AMS.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Services.AMS.Data.Mappers
{
    public class MapperFactory : IMapperFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MapperFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMapper<TOut, TIn> Get<TOut, TIn>()
        {
            IMapper<TOut, TIn> service = _serviceProvider.GetService<IMapper<TOut, TIn>>();

            return service ??
                throw new MapperNotFoundException($"Mapper<{typeof(TOut).Name}, " +
                    $"{typeof(TIn).Name}> not found.");
        }
    }
}
