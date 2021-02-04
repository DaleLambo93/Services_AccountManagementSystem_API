using DL.Services.AMS.Data.Context;
using DL.Services.AMS.Data.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace DL.Services.AMS.IntegrationTests.AccountController
{
    public class TestServerFixture
    {
        private TestServer _testServer;
        public HttpClient Client { get; private set; }
        public Uri BaseAddress { get; set; }
        public AMSDbContext DbContext { get; private set; }
        public IMapperFactory Mapper { get; private set; }

        public TestServerFixture()
        {
            _testServer = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build())
                .UseContentRoot(GetContentRootPath())
                .UseStartup<TestStartup>());

            IServiceProvider services = _testServer.Host.Services;
            Client = _testServer.CreateClient();
            BaseAddress = new Uri($"http://localhost/v1/Account");
            Mapper = services.GetService<IMapperFactory>();

            DbContext = services.GetService(typeof(AMSDbContext)) as AMSDbContext;
            DbContext.Database.EnsureCreated();
        }

        private string GetContentRootPath()
        {
            var relativePathToWebProject = @"../../../../../src/DL.Services.AMS.API";
            return relativePathToWebProject;
        }
    }
}
