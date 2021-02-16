using DL.Services.AMS.Domain.UseCases;
using DL.Services.AMS.Domain.UseCases.AccountDetails.Edit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountDetailsController : Controller
    {
        private readonly IUseCaseFactory _useCaseFactory;
        private readonly ILogger<AccountDetailsController> _logger;
        public AccountDetailsController(IUseCaseFactory useCaseFactory,
            ILogger<AccountDetailsController> logger)
        {
            _useCaseFactory = useCaseFactory;
            _logger = logger;
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditAccountDetailsRequest request)
        {
            _logger.LogInformation($"Sending request to EditAccountDetailsUseCase {JsonConvert.SerializeObject(request)}.");
            var response = await _useCaseFactory.Get<EditAccountDetailsRequest, EditAccountDetailsResponse>()
                .Handle(request);
            _logger.LogInformation($"Response received from EditAccountDetailsUseCase {JsonConvert.SerializeObject(response)}.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Edit account details was not successful: {response.Reason}");
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response.AccountDetailsEntity);
        }
    }
}
