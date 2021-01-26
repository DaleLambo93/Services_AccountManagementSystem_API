using DL.Services.AMS.Domain.Entities;
using DL.Services.AMS.Domain.Entities.Constants;
using DL.Services.AMS.Domain.UseCases.AccountDetails.Edit;
using DL.Services.AMS.IntegrationTests.Helpers;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace DL.Services.AMS.IntegrationTests.AccountDetailsController
{
    public class HttpPutTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServerFixture;

        public HttpPutTests(TestServerFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
        }

        [Fact]
        public async void Edit_AccountDetailsNotFound_Returns404()
        {
            //Arrange
            var request = new EditAccountDetailsRequest()
            {
                AccountId = 12349
            };
            var json = JsonConvert.SerializeObject(request);
            var jsonRequest = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await _testServerFixture.Client.PutAsync($"{_testServerFixture.BaseAddress}/Edit", jsonRequest);
            var account = _testServerFixture.DbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountId);

            //Assert
            Assert.Null(account);
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Edit_AccountDetailsUpdated_Returns200()
        {
            //Arrange
            int accountId = 6, detailsId = 6;
            string format = "dd/MM/yyyy";
            var request = RequestHelper.EditAccountDetailsRequest(accountId);
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var account = ModelHelper.CreateAccount(accountId, detailsId, Reference.Operative, AccountStatus.Active);
            _testServerFixture.DbContext.AddAccount(account);

            //Act
            var response = await _testServerFixture.Client.PutAsync($"{_testServerFixture.BaseAddress}/Edit", jsonRequest);
            var content = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<AccountDetailsEntity>(content);

            //Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);

            Assert.NotNull(actualResponse.ModifiedAt);
            Assert.Equal(request.FirstName, actualResponse.FirstName);
            Assert.Equal(request.Surname, actualResponse.Surname);
            Assert.Equal(request.DateOfBirth.ToString(format), actualResponse.DateOfBirth.ToString(format));
            Assert.Equal(request.AddressLine1, actualResponse.AddressLine1);
            Assert.Equal(request.AddressLine2, actualResponse.AddressLine2);
            Assert.Equal(request.PostCode, actualResponse.PostCode);
            Assert.Equal(request.Country, actualResponse.Country);
            Assert.Equal(request.EmailAddress, actualResponse.EmailAddress);
        }
    }
}
