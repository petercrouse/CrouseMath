using CrouseMath.Application.Common.Interfaces;
using System;

namespace CrouseMath.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
