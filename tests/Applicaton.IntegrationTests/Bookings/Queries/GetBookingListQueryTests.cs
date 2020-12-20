using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Queries.GetBookingList;
using CrouseMath.Infrastructure.Persistence;
using CrouseMath.Application.IntegrationTests;
using NUnit.Framework;
using CrouseMath.Domain.Entities;
using System;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using FluentAssertions;

namespace CrouseMath.Application.UnitTests.Bookings.Queries
{
    using static Testing;

    public class GetBookingListQueryTests : TestBase
    {
        [Test]
        public async Task GetBookingList()
        {
            await AddAsync(new ExtraClass
            {
                Name = "Gandalf's class",
                Size = 1,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
                IsClassFull = false
            });

            await AddAsync(new Booking
            {
                ExtraClassId = 1,
                Paid = false,
                BookingPrice = 500,
                UserId = "11111"
            });

            await AddAsync(new Booking
            {
                ExtraClassId = 1,
                Paid = false,
                BookingPrice = 500,
                UserId = "22222"
            });

            var query = new GetBookingListQuery();

            var result = await SendAsync(query);

            result.Should().BeOfType<BookingListViewModel>();

            result.Bookings.Should().HaveCount(2);
        }
    }
}
