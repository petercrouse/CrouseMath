using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.UnitTests.Bookings.Commands.CreateBooking
{
    using static Testing;

    public class CreateBookingCommandTests : TestBase
    {
        [Test]
        public async Task CreateBookingCommand_ShouldExecuteSuccessfully()
        {
            // Arrange
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

            var booking = await FindAsync<Booking>(bookingId);

            booking.UserId.Should().Be(userId);
            booking.ExtraClassId.Should().Be(1);
        }

        [Test]
        public void CreateBookingCommandHandler_ExtraClassDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CreateBookingCommand { ExtraClassId = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task CreateBookingCommandHandler_ShouldThrowClassIsFullException()
        {
            // Arrange
            await AddAsync(new ExtraClass
            {
                Name = "Gandalf's class",
                Size = 1,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
                IsClassFull = true
            });

            var command = new CreateBookingCommand { ExtraClassId = 1 };

            // Assert

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowExactly<ClassIsFullException>();
        }

        [Test]
        public async Task CreateBookingCommandHandler_ShouldThrowDoubleBookingException()
        {
            // Arrange
            var userId = await RunAsDefaultUserAsync();

            await AddAsync(new ExtraClass
            {
                Name = "Gandalf's class",
                Size = 5,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
                IsClassFull = false
            });

            var command = new CreateBookingCommand { ExtraClassId = 1 };

            var bookingId = await SendAsync(command);

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<DoubleBookingException>();
        }
    }
}
