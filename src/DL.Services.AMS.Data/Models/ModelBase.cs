using System;
using System.ComponentModel.DataAnnotations;

namespace DL.Services.AMS.Data.Models
{
    public class ModelBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
