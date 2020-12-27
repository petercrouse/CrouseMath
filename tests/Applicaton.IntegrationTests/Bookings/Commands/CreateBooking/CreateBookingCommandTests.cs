using System;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.IntegrationTests.Bookings.Commands.CreateBooking
{
    using static Testing;

    public class CreateBookingCommandTests : TestBase
    {
        [Test]
        public async Task CreateBookingCommand_ShouldExecuteSuccessfully()
        {
            // Arrange
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

            var booking = await FindAsync<Booking>(bookingId);

            booking.UserId.Should().Be(userId);
            booking.ExtraClassId.Should().Be(extraClassId);
            booking.CreatedBy.Should().Be(userId);
            booking.Created.Should().BeCloseTo(DateTime.Now, 10000);
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

            await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId });
            await RunAsUserAsync("student@local", "Student1234!");
            var command = new CreateBookingCommand { ExtraClassId = extraClassId };

            // Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ClassIsFullException>();
        }

        [Test]
        public async Task CreateBookingCommandHandler_ShouldThrowDoubleBookingException()
        {
            // Arrange
            var userId = await RunAsDefaultUserAsync();
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "Wizardry" });
            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                Name = "Gandalf's class",
                SubjectId = subjectId,
                Size = 5,
                Price = 500,
                Duration = TimeSpan.FromMinutes(60),
                Date = DateTime.Now,
            });

            var command = new CreateBookingCommand { ExtraClassId = extraClassId };

            var bookingId = await SendAsync(command);

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<DoubleBookingException>();
        }
    }
}
