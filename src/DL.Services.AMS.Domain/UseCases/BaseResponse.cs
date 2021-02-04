using System.Net;
using System.Text.Json.Serialization;

namespace DL.Services.AMS.Domain.UseCases
{
    public class BaseResponse
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        [JsonIgnore]
        public string Reason { get; set; } = "No information provided";
    }
}
