using System;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Bookings.Commands.UpdateBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.IntegrationTests.Bookings.Commands.UpdateBooking
{
    using static Testing;

    public class UpdateBookingCommandTests : TestBase
    {
        [Test]
        public async Task UpdateBookingCommand_ShouldUpdateBooking()
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

            var bookingId = await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId });

            var command = new UpdateBookingCommand
            {
                Id = bookingId,
                ExtraClassId = extraClassId,
                Paid = true,
                Price = 100
            };

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

            var command = new CreateBookingCommand { ExtraClassId = extraClassId };

            var bookingId = await SendAsync(command);

            var updateCommand = new UpdateBookingCommand { Id = bookingId, ExtraClassId = 9999999999, Paid = true, Price = 100 };

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(updateCommand)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task UpdateBookingCommand_ShouldThrowDoubleBookingException()
        {
            //Arrange
            var userId = await RunAsDefaultUserAsync();
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "Wizardry" });
            var extraClassId1 = await SendAsync(new CreateExtraClassCommand
            {
                Name = "Gandalf's class",
                SubjectId = subjectId,
                Size = 2,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
            });

            var extraClassId2 = await SendAsync(new CreateExtraClassCommand
            {
                Name = "Saruman's class",
                SubjectId = subjectId,
                Size = 2,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now
            });

            var bookingId1 = await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId1 });
            var bookingId2 = await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId2 });

            var command = new UpdateBookingCommand { Id = bookingId1, ExtraClassId = extraClassId2, Price = 500, Paid = false };

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<DoubleBookingException>();
        }
    }
}
