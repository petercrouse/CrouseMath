using System.Threading;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Common.Exceptions;
using Xunit;

namespace CrouseMath.Application.UnitTests.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandTests : CommandTestBase
    {
        [Fact]
        public async void CreateBookingCommandHandler_ShouldThrowNotFoundException()
        {
            // Arrange
            var sut = new CreateBookingCommand.CreateBookingCommandHandler(Context);
            var extraClassId = 999;
            var studentId = 1;

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new CreateBookingCommand
            {
                ExtraClassId = extraClassId,
                StudentId = studentId
            }, CancellationToken.None));
        }

        [Fact]
        public async void CreateBookingCommandHandler_ShouldThrowClassIsFullException()
        {
            // Arrange
            var sut = new CreateBookingCommand.CreateBookingCommandHandler(Context);
            var extraClassId = 3;
            var studentId = 4;

            // Assert
            await Assert.ThrowsAsync<ClassIsFullException>(async () => await sut.Handle(new CreateBookingCommand
            {
                ExtraClassId = extraClassId,
                StudentId = studentId
            }, CancellationToken.None));
        }

        [Fact]
        public async void CreateBookingCommandHandler_ShouldThrowDoubleBookingException()
        {
            // Arrange
            var sut = new CreateBookingCommand.CreateBookingCommandHandler(Context);
            var extraClassId = 1;
            var studentId = 1;

            // Assert
            await Assert.ThrowsAsync<DoubleBookingException>(async () => await sut.Handle(new CreateBookingCommand
            {
                ExtraClassId = extraClassId,
                StudentId = studentId
            }, CancellationToken.None));
        }
    }
}
