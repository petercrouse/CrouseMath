using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Bookings.Commands.DeleteBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.UnitTests.Bookings.Commands.DeleteBooking
{
    using static Testing;

    public class DeleteBookingCommandTests: TestBase
    {
        [Test]
        public void DeleteBookingCommand_ShouldThrowNotFoundException()
        {
            var command = new DeleteBookingCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<DeleteFailureException>();
        }

        [Test]
        public async Task DeleteBookingCommand_ShouldDeleteSuccessfully()
        {
            await AddAsync(new Subject { Name = "Math" });

            await AddAsync(new ExtraClass
            {
                Name = "Math Algebra",
                Size = 5,
                SubjectId = 1,
                Date = DateTime.Now,
                Duration = new TimeSpan(1, 0, 0),
                IsClassFull = false,
                Price = 100,
            });

            var command = new CreateBookingCommand { ExtraClassId = 1 };

            var bookingId = await SendAsync(command);

            await SendAsync(new DeleteBookingCommand { Id = bookingId });

            var booking = await FindAsync<Booking>(bookingId);
            booking.Should().BeNull();
        }
    }
}
