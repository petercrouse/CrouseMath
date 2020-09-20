using System.Threading;
using CrouseMath.Application.Bookings.Commands.DeleteBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.UnitTests;
using Xunit;

namespace CrouseMath.Application.UnitTests.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandTests: CommandTestBase
    {
        [Fact]
        public async void DeleteBookingCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var sut = new DeleteBookingCommand.DeleteBookingCommandHandler(Context);
            var command = new DeleteBookingCommand { Id = 99 };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }
    }
}
