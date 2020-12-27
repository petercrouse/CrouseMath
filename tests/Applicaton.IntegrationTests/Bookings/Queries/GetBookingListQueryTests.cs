using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Queries.GetBookingList;
using NUnit.Framework;
using CrouseMath.Domain.Entities;
using System;
using FluentAssertions;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;

namespace CrouseMath.Application.IntegrationTests.Bookings.Queries
{
    using static Testing;

    public class GetBookingListQueryTests : TestBase
    {
        [Test]
        public async Task GetBookingList()
        {
            var userId = await RunAsDefaultUserAsync();
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "Wizardry" });
            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                Name = "Gandalf's class",
                SubjectId = subjectId,
                Size = 1,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
            });

            await AddAsync(new Booking
            {
                ExtraClassId = extraClassId,
                Paid = false,
                BookingPrice = 500,
                UserId = userId
            });

            await AddAsync(new Booking
            {
                ExtraClassId = extraClassId,
                Paid = false,
                BookingPrice = 500,
                UserId = userId
            });

            var query = new GetBookingListQuery();

            var result = await SendAsync(query);

            result.Should().BeOfType<BookingListViewModel>();

            result.Bookings.Should().HaveCount(2);
        }
    }
}
