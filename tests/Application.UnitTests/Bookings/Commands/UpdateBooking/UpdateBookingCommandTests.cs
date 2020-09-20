using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.UpdateBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.UnitTests;
using Xunit;

namespace CrouseMath.Application.UnitTests.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandTests : CommandTestBase
    {
        [Fact]
        public async Task UpdateBookingCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var sut = new UpdateBookingCommand.UpdateBookingCommandHandler(Context);
            var command = new UpdateBookingCommand
            {
                Id = 99,
                ExtraClassId = 1
            };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateBookingCommand_ShouldThrowNotFoundException_MissingExtraClass()
        {
            //Arrange
            var sut = new UpdateBookingCommand.UpdateBookingCommandHandler(Context);
            var command = new UpdateBookingCommand
            {
                Id = 1,
                ExtraClassId = 99
            };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateBookingCommand_ShouldThrowDoubleBookingException()
        {
            //Arrange
            var sut = new UpdateBookingCommand.UpdateBookingCommandHandler(Context);
            var command = new UpdateBookingCommand
            {
                Id = 1,
                ExtraClassId = 3
            };

            //Assert
            await Assert.ThrowsAnyAsync<DoubleBookingException>(async () => await sut.Handle(command, CancellationToken.None));
        }
    }
}
