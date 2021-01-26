using System.ComponentModel.DataAnnotations;

namespace DL.Services.AMS.Domain.UseCases.Account.Fetch
{
    public class FetchAccountRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Account Id cannot be default value 0.")]
        public int AccountId { get; set; }
    }
}
