using DL.Services.AMS.Domain.Entities.Constants;

namespace DL.Services.AMS.Data.Models
{
    public class Account : ModelBase
    {
        public string Username { get; set; }
        public string CustomerReference { get; set; }
        public AccountStatus Status { get; set; }
        public AccountDetails AccountDetails { get; set; }
    }
}
