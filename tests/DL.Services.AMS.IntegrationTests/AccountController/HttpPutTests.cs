using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.UseCases.Account.Confirm;
using DL.Services.AMS.IntegrationTests.Helpers;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using Xunit;

namespace DL.Services.AMS.IntegrationTests.AccountController
{
    public class HttpPutTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServerFixture;

        public HttpPutTests(TestServerFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
        }

        [Fact]
        public async void Confirm_AccountNotFound_Returns404()
        {
            //Arrange
            int accountId = 12346;

            //Act
            var response = await _testServerFixture.Client.PutAsync($"{_testServerFixture.BaseAddress}/{accountId}/Confirm", null);
            var account = _testServerFixture.DbContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            //Assert
            Assert.Null(account);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Confirm_AccountStatusActive_Returns200()
        {
            //Arrange
            int accountId = 2;

            var account = ModelHelper.CreateAccount(accountId, null, Reference.Email, AccountStatus.Pending);
            _testServerFixture.DbContext.AddAccount(account);

            //Act
            var response = await _testServerFixture.Client.PutAsync($"{_testServerFixture.BaseAddress}/{accountId}/Confirm", null);
            var content = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<ConfirmAccountResponse>(content);
            
            //Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(account.Id, actualResponse.AccountId);
            Assert.Equal(AccountStatus.Active, actualResponse.Status);
        }


        [Fact]
        public async void Cancel_AccountNotFound_Returns404()
        {
            //Arrange
            int accountId = 12347;

            //Act
            var response = await _testServerFixture.Client.PutAsync($"{_testServerFixture.BaseAddress}/{accountId}/Cancel", null);
            var account = _testServerFixture.DbContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            //Assert
            Assert.Null(account);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Cancel_AcccountStatusCancelled_Returns200()
        {
            //Arrange
            int accountId = 3;

            var account = ModelHelper.CreateAccount(accountId, null, Reference.Operative, AccountStatus.Active);
            _testServerFixture.DbContext.AddAccount(account);

            //Act
            var response = await _testServerFixture.Client.PutAsync($"{_testServerFixture.BaseAddress}/{accountId}/Cancel", null);
            var content = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<ConfirmAccountResponse>(content);

            //Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.Equal(account.Id, actualResponse.AccountId);
            Assert.Equal(AccountStatus.Cancelled, actualResponse.Status);
        }
    }
}
