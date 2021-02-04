﻿using DL.Services.AMS.Domain.Entities.Constants;

namespace DL.Services.AMS.Domain.UseCases.Account.Create
{
    public class CreateAccountResponse : BaseResponse
    {
        public int AccountId { get; set; }
        public AccountStatus Status { get; set; }

    }
}
