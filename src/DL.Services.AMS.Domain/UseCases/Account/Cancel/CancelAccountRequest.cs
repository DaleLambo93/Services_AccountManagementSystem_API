using System.ComponentModel.DataAnnotations;

namespace DL.Services.AMS.Domain.UseCases.Account.Cancel
{
    public class CancelAccountRequest
    {
        [Required]
        public int AccountId { get; set; }
    }
}
