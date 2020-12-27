using System;

namespace CrouseMath.Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime Today { get; }
        int Compare(DateTime dateTime1, DateTime dateTime2);
    }
}
