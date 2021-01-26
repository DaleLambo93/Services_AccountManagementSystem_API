using System;

namespace DL.Services.AMS.Domain.Exceptions
{
    [Serializable]
    public class MapperNotFoundException : Exception
    {
        public MapperNotFoundException(string message) : base(message)
        {
        }
    }
}
