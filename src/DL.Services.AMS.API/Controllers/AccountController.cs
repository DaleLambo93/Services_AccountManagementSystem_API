using DL.Services.AMS.Domain.UseCases;
using DL.Services.AMS.Domain.UseCases.Account.Cancel;
using DL.Services.AMS.Domain.UseCases.Account.Confirm;
using DL.Services.AMS.Domain.UseCases.Account.Create;
using DL.Services.AMS.Domain.UseCases.Account.Fetch;
using DL.Services.AMS.Domain.UseCases.Account.Remove;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUseCaseFactory _useCaseFactory; //Use factories or facades to reduce constructor dependencies
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUseCaseFactory useCaseFactory,
            ILogger<AccountController> logger)
        {
            _useCaseFactory = useCaseFactory;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            _logger.LogInformation($"Sending request to CreateAccountUseCase {JsonConvert.SerializeObject(request)}.");
            var response = await _useCaseFactory.Get<CreateAccountRequest, CreateAccountResponse>()
                .Handle(request);
            _logger.LogInformation($"Response received from CreateAccountUseCase {JsonConvert.SerializeObject(response)}.");

            return Created("", response);
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> Get(int accountId)
        {
            _logger.LogInformation($"Sending request to FetchAccountUseCase with AccountId {accountId}.");
            var response = await _useCaseFactory.Get<FetchAccountRequest, FetchAccountResponse>()
                .Handle(new FetchAccountRequest() 
                {
                    AccountId = accountId
                });
            _logger.LogInformation($"Response received from FetchAccountUseCase {JsonConvert.SerializeObject(response)}.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Fetch account was not successful: {response.Reason}");
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response.AccountEntity);
        }

        [HttpPut("{accountId}/Confirm")]
        public async Task<IActionResult> Confirm(int accountId)
        {
            _logger.LogInformation($"Sending request to ConfirmAccountUseCase with AccountId {accountId}.");
            var response = await _useCaseFactory.Get<ConfirmAccountRequest, ConfirmAccountResponse>()
                .Handle(new ConfirmAccountRequest() 
                {
                    AccountId = accountId
                });
            _logger.LogInformation($"Response received from ConfirmAccountUseCase {JsonConvert.SerializeObject(response)}.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Confirm account was not successful: {response.Reason}");
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response);
        }

        [HttpPut("{accountId}/Cancel")]
        public async Task<IActionResult> Cancel(int accountId)
        {
            _logger.LogInformation($"Sending request to CancelAccountUseCase with AccountId {accountId}.");
            var response = await _useCaseFactory.Get<CancelAccountRequest, CancelAccountResponse>()
                .Handle(new CancelAccountRequest() 
                {
                    AccountId = accountId
                });
            _logger.LogInformation($"Response received from CancelAccountUseCase {JsonConvert.SerializeObject(response)}.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Cancel account was not successful: {response.Reason}");
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response);
        }

        [HttpDelete("{accountId}/Remove")]
        public async Task<IActionResult> Remove(int accountId)
        {
            _logger.LogInformation($"Sending request to RemoveAccountUseCase with AccountId {accountId}.");
            var response = await _useCaseFactory.Get<RemoveAccountRequest, BaseResponse>()
                .Handle(new RemoveAccountRequest() 
                {
                    AccountId = accountId
                });
            _logger.LogInformation($"Response received from RemoveAccountUseCase {JsonConvert.SerializeObject(response)}.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Remove account was not successful: {response.Reason}");
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response.Reason);
        }
    }
}
