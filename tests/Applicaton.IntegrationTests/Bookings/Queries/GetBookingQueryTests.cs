using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Bookings.Queries.GetBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CrouseMath.Application.IntegrationTests.Bookings.Queries
{
    using static Testing;

    public class GetBookingQueryTests : TestBase
    {
        [Test]
        public async Task GetBooking()
        {
            var userId = await RunAsDefaultUserAsync();
            var className = "Gandalf's class";
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "Wizardry" });
            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                Name = className,
                SubjectId = subjectId,
                Size = 1,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
            });

            var bookingId = await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId });

            var result = await SendAsync(new GetBookingQuery { Id = bookingId });

            result.Should().BeOfType<BookingViewModel>();
            result.Booking.Id.Should().Be(bookingId);
            result.Booking.ExtraClassName.Should().Be(className);
            result.Booking.UserId.Should().Be(userId);
        }

        [Test]
        public void GetBooking_ShouldThrowNotFoundException()
        {
            var query = new GetBookingQuery { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }
    }
}
