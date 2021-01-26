using System;

namespace DL.Services.AMS.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
