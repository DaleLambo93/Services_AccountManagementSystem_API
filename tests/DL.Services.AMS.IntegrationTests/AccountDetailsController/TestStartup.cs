using DL.Services.AMS.Data.Extensions;
using DL.Services.AMS.Domain.Extensions;
using DL.Services.AMS.IntegrationTests.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DL.Services.AMS.IntegrationTests.AccountDetailsController
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; }

        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            
            services.AddDomainServices();
            services.AddInMemoryDb();
            services.AddDataServices();

            services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("DL.Services.AMS.API")));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

