using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Bookings.Commands.UpdateBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.UnitTests.Bookings.Commands.UpdateBooking
{
    using static Testing;

    public class UpdateBookingCommandTests : TestBase
    {
        [Test]
        public async Task UpdateBookingCommand_ShouldUpdateBooking()
        {
            var userId = await RunAsDefaultUserAsync();

            await AddAsync(new ExtraClass
            {
                Name = "Gandalf's class",
                Size = 1,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
                IsClassFull = false
            });

            var bookingId = await SendAsync(new CreateBookingCommand {ExtraClassId = 1});

            var command = new UpdateBookingCommand { Id = bookingId, ExtraClassId = 1, Paid = true, Price = 100 };

            await SendAsync(command);

            var booking = await FindAsync<Booking>(bookingId);

            booking.Paid.Should().BeTrue();
            booking.BookingPrice.Should().Be(100);
            booking.LastModifiedBy.Should().NotBeNull();
            booking.LastModifiedBy.Should().Be(userId);
            booking.LastModified.Should().NotBeNull();
            booking.LastModified.Should().BeCloseTo(DateTime.Now, 10000);
        }

        [Test]
        public void UpdateBookingCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var command = new UpdateBookingCommand { Id = 1, ExtraClassId = 1, Paid = true, Price = 100 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task UpdateBookingCommand_ShouldThrowNotFoundException_MissingExtraClass()
        {
            var userId = await RunAsDefaultUserAsync();

            await AddAsync(new ExtraClass
            {
                Name = "Gandalf's class",
                Size = 1,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
                IsClassFull = false
            });

            var command = new CreateBookingCommand { ExtraClassId = 1 };

            var bookingId = await SendAsync(command);

            var updateCommand = new UpdateBookingCommand { Id = bookingId, ExtraClassId = 99, Paid = true, Price = 100 };

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(updateCommand)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task UpdateBookingCommand_ShouldThrowDoubleBookingException()
        {
            //Arrange
            var userId = await RunAsDefaultUserAsync();

            await AddAsync(new ExtraClass
            {
                Name = "Gandalf's class",
                Size = 2,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
                IsClassFull = false
            });

            await AddAsync(new ExtraClass
            {
                Name = "Saruman's class",
                Size = 2,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
                IsClassFull = false
            });

            var bookingIdClass1 = await SendAsync(new CreateBookingCommand { ExtraClassId = 1 });
            var bookingIdClass2 = await SendAsync(new CreateBookingCommand { ExtraClassId = 2 });

            var command = new UpdateBookingCommand { Id = bookingIdClass1, ExtraClassId = 2, Price = 500, Paid = false };

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<DoubleBookingException>();
        }
    }
}
