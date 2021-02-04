using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.UseCases.Account.Create;
using DL.Services.AMS.IntegrationTests.Helpers;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace DL.Services.AMS.IntegrationTests.AccountController
{
    public class HttpPostTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServerFixture;

        public HttpPostTests(TestServerFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
        }

        [Fact]
        public async void Create_AccountCreated_Returns201()
        {
            //Arrange
            var request = RequestHelper.CreateAccountRequest(4, Reference.Test);
            var json = JsonConvert.SerializeObject(request);
            var jsonRequest = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await _testServerFixture.Client.PostAsync($"{_testServerFixture.BaseAddress}/Create", jsonRequest);
            var content = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<CreateAccountResponse>(content);

            var account = _testServerFixture.DbContext.Accounts.FirstOrDefault(x => x.Username == request.Username);

            //Assert
            Assert.NotNull(account);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.Equal(account.Id, actualResponse.AccountId);
            Assert.Equal(AccountStatus.Pending, actualResponse.Status);
        }
    }
}
