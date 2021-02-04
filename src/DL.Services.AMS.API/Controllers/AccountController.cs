using DL.Services.AMS.Domain.UseCases;
using DL.Services.AMS.Domain.UseCases.Account.Cancel;
using DL.Services.AMS.Domain.UseCases.Account.Confirm;
using DL.Services.AMS.Domain.UseCases.Account.Create;
using DL.Services.AMS.Domain.UseCases.Account.Fetch;
using DL.Services.AMS.Domain.UseCases.Account.Remove;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUseCaseFactory _useCaseFactory; //Use factories or facades to reduce constructor dependencies
        public AccountController(IUseCaseFactory useCaseFactory)
        {
            _useCaseFactory = useCaseFactory;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            var response = await _useCaseFactory.Get<CreateAccountRequest, CreateAccountResponse>()
                .Handle(request);

            return Created("", response);
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> Get(int accountId)
        {
            var response = await _useCaseFactory.Get<FetchAccountRequest, FetchAccountResponse>()
                .Handle(new FetchAccountRequest() 
                {
                    AccountId = accountId
                });

            if (response.StatusCode != HttpStatusCode.OK)
            {                
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response.AccountEntity);
        }

        [HttpPut("{accountId}/Confirm")]
        public async Task<IActionResult> Confirm(int accountId)
        {
            var response = await _useCaseFactory.Get<ConfirmAccountRequest, ConfirmAccountResponse>()
                .Handle(new ConfirmAccountRequest() 
                {
                    AccountId = accountId
                });

            if (response.StatusCode != HttpStatusCode.OK)
            {                
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response);
        }

        [HttpPut("{accountId}/Cancel")]
        public async Task<IActionResult> Cancel(int accountId)
        {
            var response = await _useCaseFactory.Get<CancelAccountRequest, CancelAccountResponse>()
                .Handle(new CancelAccountRequest() 
                {
                    AccountId = accountId
                });

            if (response.StatusCode != HttpStatusCode.OK)
            {                
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response);
        }

        [HttpDelete("{accountId}/Remove")]
        public async Task<IActionResult> Remove(int accountId)
        {
            var response = await _useCaseFactory.Get<RemoveAccountRequest, BaseResponse>()
                .Handle(new RemoveAccountRequest() 
                {
                    AccountId = accountId
                });

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response.Reason);
        }
    }
}
