using System;

namespace CrouseMath.Application.Common.Exceptions
{
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(object name, object key, object userId)
            :base($"Entity \"{name}\" ({key}) already has a booking for user ({userId})")
        {
        }
    }
}