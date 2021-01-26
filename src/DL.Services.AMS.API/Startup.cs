using DL.Services.AMS.API.Extensions;
using DL.Services.AMS.Data.Extensions;
using DL.Services.AMS.Domain.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DL.Services.AMS.API
{
    public class Startup
    {
        private IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _env = env;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();

            if (!_env.IsStaging() || !_env.IsProduction())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "DL.Services.AMS.API",
                        Version = "v1"
                    });
                });
            }

            services.AddLogging(config => config.AddLoggingBuilderExtensions(Configuration));

            services.AddDomainServices(Configuration);
            services.AddDataServices(Configuration);
        }
        
        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory)
        {
            if (!_env.IsStaging() || !_env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AMS API v1"));
            }

            app.ConfigureExceptionHandler(loggerFactory);
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
