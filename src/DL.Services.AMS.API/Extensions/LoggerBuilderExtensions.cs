using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace DL.Services.AMS.API.Extensions
{
    public static class LoggerBuilderExtensions
    {
        public static void AddLoggingBuilderExtensions(this ILoggingBuilder loggingBuilder,
            IConfiguration configuration)
        {
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();

            loggingBuilder.AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Error);
            loggingBuilder.AddFilter(" Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogLevel.Error);
            loggingBuilder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            loggingBuilder.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
        }
    }
}
