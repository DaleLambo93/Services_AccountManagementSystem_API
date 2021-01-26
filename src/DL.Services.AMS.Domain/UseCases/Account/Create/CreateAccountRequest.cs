using DL.Services.AMS.Domain.Entities.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace DL.Services.AMS.Domain.UseCases.Account.Create
{
    public class CreateAccountRequest
    {
        [Required]
        public Reference Reference { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$",
            ErrorMessage = "Please enter username without spaces or special characters")]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(100)]
        public string AddressLine1 { get; set; }
        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The email format is not valid")]
        public string EmailAddress { get; set; }
    }
}
