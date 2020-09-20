using System;

namespace CrouseMath.Application.Common.Exceptions
{
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(object name, object key, object studentKey)
            :base($"Entity \"{name}\" ({key}) already has a booking for student ({studentKey})")
        {
        }
    }
}