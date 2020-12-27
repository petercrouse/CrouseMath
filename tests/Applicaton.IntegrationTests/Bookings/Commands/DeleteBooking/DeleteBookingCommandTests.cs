using System;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Bookings.Commands.DeleteBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.IntegrationTests.Bookings.Commands.DeleteBooking
{
    using static Testing;

    public class DeleteBookingCommandTests : TestBase
    {
        [Test]
        public void DeleteBookingCommand_ShouldThrowNotFoundException()
        {
            var command = new DeleteBookingCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task DeleteBookingCommand_ShouldDeleteSuccessfully()
        {
            await RunAsDefaultUserAsync();
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

            await SendAsync(new DeleteBookingCommand { Id = bookingId });

            var booking = await FindAsync<Booking>(bookingId);
            booking.Should().BeNull();
        }
    }
}
