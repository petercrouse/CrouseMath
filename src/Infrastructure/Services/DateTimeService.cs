using CrouseMath.Application.Common.Interfaces;
using System;

namespace CrouseMath.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime Today => DateTime.Today;

        public int Compare(DateTime dateTime1, DateTime dateTime2)
        {
            return DateTime.Compare(dateTime1, dateTime2);
        }
    }
}
