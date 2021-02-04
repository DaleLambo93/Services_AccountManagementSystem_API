using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL.Services.AMS.Data.Models
{
    public class AccountDetails : ModelBase
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set;}
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
