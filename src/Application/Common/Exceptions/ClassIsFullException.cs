using System;

namespace CrouseMath.Application.Common.Exceptions
{
    public class ClassIsFullException : Exception
    {
        public ClassIsFullException(object name, object key)
            : base($"Unable to create booking for \"{name}\" ({key}), the class is full ")
        {
        }
    }
}