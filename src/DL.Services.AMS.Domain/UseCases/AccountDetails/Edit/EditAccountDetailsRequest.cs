using System;
using System.ComponentModel.DataAnnotations;

namespace DL.Services.AMS.Domain.UseCases.AccountDetails.Edit
{
    public class EditAccountDetailsRequest
    {
        [Required]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
    }
}
