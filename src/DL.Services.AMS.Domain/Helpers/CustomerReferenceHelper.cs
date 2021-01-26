using DL.Services.AMS.Domain.Entities.Constants;
using System;

namespace DL.Services.AMS.Domain.Helpers
{
    public static class CustomerReferenceHelper
    {
        public static string GetCustomerReference(Reference reference)
        {
            var guid = $"{Guid.NewGuid().ToString()}_{DateTime.Now.ToString("ddMMyyyy-hhmmssfff")}";

            switch (reference)
            {
                case Reference.Test:
                    return $"{ReferenceCode.Test}{guid}";
                case Reference.Website:
                    return $"{ReferenceCode.Website}{guid}";
                case Reference.Operative:
                    return $"{ReferenceCode.Operative}{guid}";
                case Reference.Email:
                    return $"{ReferenceCode.Email}{guid}";
                case Reference.Advertisement:
                    return $"{ReferenceCode.Advertisement}{guid}";
                default:
                    throw new Exception($"Reference {reference} not found.");
            }
        }
    }
}
