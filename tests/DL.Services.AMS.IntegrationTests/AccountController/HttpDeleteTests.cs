using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.IntegrationTests.Helpers;
using System.Linq;
using System.Net;
using Xunit;

namespace DL.Services.AMS.IntegrationTests.AccountController
{
    public class HttpDeleteTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServerFixture;

        public HttpDeleteTests(TestServerFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
        }

        [Fact]
        public async void Remove_AccountNotFound_Returns404()
        {
            //Arrange
            int accountId = 12348;

            //Act
            var response = await _testServerFixture.Client.DeleteAsync($"{_testServerFixture.BaseAddress}/{accountId}/Remove");
            var account = _testServerFixture.DbContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            //Assert
            Assert.Null(account);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Remove_AccountDeleted_Returns200()
        {
            //Arrange
            int accountId = 5;
            string expectedResponse = $"Account with Id {accountId} was deleted.";
            var account = ModelHelper.CreateAccount(accountId, null, Reference.Advertisement, AccountStatus.Active);
            _testServerFixture.DbContext.AddAccount(account);

            //Act
            var response = await _testServerFixture.Client.DeleteAsync($"{_testServerFixture.BaseAddress}/{accountId}/Remove");
            var actualResponse = await response.Content.ReadAsStringAsync();           

            var deletedAccount = _testServerFixture.DbContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            //Assert
            Assert.Null(deletedAccount);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(expectedResponse, actualResponse);
        }
    }
}
