using DL.Services.AMS.Domain.UseCases;
using DL.Services.AMS.Domain.UseCases.AccountDetails.Edit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DL.Services.AMS.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountDetailsController : Controller
    {
        private readonly IUseCaseFactory _useCaseFactory;
        public AccountDetailsController(IUseCaseFactory useCaseFactory)
        {
            _useCaseFactory = useCaseFactory;
        }

        [HttpPut()]
        public async Task<IActionResult> Edit([FromBody] EditAccountDetailsRequest request)
        {
            var response = await _useCaseFactory.Get<EditAccountDetailsRequest, EditAccountDetailsResponse>()
                .Handle(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, response.Reason);
            }

            return Ok(response);
        }
    }
}
