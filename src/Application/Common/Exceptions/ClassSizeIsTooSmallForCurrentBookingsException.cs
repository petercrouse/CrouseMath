using System;

namespace CrouseMath.Application.Common.Exceptions
{
    public class ClassSizeIsTooSmallForCurrentBookingsException : Exception
    {
        public ClassSizeIsTooSmallForCurrentBookingsException(object name, object key, int currentSize, int newSize)
            : base($"Entity \"{name}\" ({key}) cant be updated to a class size of {newSize} because there are {currentSize} bookings for the class.")
        {
        }
    }
}