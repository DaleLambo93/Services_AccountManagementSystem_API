using System.ComponentModel.DataAnnotations;

namespace DL.Services.AMS.Domain.UseCases.Account.Confirm
{
    public class ConfirmAccountRequest
    {
        [Required]
        public int AccountId { get; set; }
    }
}
