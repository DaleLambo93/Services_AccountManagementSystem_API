using DL.Services.AMS.Data.Models;
using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.IntegrationTests.Helpers;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using Xunit;

namespace DL.Services.AMS.IntegrationTests.AccountController
{
    public class HttpGetTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServerFixture;

        public HttpGetTests(TestServerFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
        }

        [Fact]
        public async void Get_AccountNotFound_Returns404()
        {
            //Arrange
            int accountId = 12345;            

            //Act
            var response = await _testServerFixture.Client.GetAsync($"{_testServerFixture.BaseAddress}/{accountId}");
            var account = _testServerFixture.DbContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            //Assert
            Assert.Null(account);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);

        }

        [Fact]
        public async void Get_AccountFound_Returns200()
        {
            //Arrange
            int accountId = 1, accountDetailsId = 1;

            var account = ModelHelper.CreateAccount(accountId, accountDetailsId, Reference.Website, AccountStatus.Active);
            _testServerFixture.DbContext.AddAccount(account);
            var expectedResponse = _testServerFixture.Mapper.Get<AccountEntity, Account>().Map(account);
            
            //Act
            var response = await _testServerFixture.Client.GetAsync($"{_testServerFixture.BaseAddress}/{accountId}");    
            var content = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<AccountEntity>(content);

            //Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
