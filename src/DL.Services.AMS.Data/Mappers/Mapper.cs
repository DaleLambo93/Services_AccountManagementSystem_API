using Microsoft.Extensions.Logging;
using System;

namespace DL.Services.AMS.Data.Mappers
{
    public abstract class Mapper<TOut, TIn> : IMapper<TOut, TIn> where TIn : class
    {
        private readonly ILogger<Mapper<TOut, TIn>> _logger;

        protected Mapper(ILogger<Mapper<TOut, TIn>> logger)
        {
            _logger = logger;
        }

        public abstract TOut Map(TIn item);

        protected virtual bool Validate(TIn item,
            Predicate<TIn> custom = null)
        {
            var mapping = $"Mapping: {typeof(TIn).Name} => {typeof(TOut).Name}";

            if (item is null)
            {
                _logger.LogWarning($"Mapping failed as {typeof(TIn).Name} was null. " + mapping);
                return false;
            }

            if (custom == null || !custom(item))
            {
                return true;
            }

            _logger.LogWarning($"Mapping failed due to custom validation." + mapping);

            return false;
        }
    }
}
