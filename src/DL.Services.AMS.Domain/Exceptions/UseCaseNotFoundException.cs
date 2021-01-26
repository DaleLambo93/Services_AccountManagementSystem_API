using System;
using System.Runtime.Serialization;

namespace DL.Services.AMS.Domain.Exceptions
{
    [Serializable]
    internal class UseCaseNotFoundException : Exception
    {
        public UseCaseNotFoundException()
        {
        }

        public UseCaseNotFoundException(string message) : base(message)
        {
        }

        public UseCaseNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UseCaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
