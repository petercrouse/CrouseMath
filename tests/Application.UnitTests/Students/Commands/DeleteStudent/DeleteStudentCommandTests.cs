using System.Threading;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Students.Commands.DeleteStudent;
using Xunit;

namespace CrouseMath.Application.UnitTests.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandTests : CommandTestBase
    {
        [Fact]
        public async void DeleteStudentCommand_ShouldThrowDeleteFailureException()
        {
            //Arrange
            var sut = new DeleteStudentCommand.DeleteStudentCommandHandler(Context);
            var command = new DeleteStudentCommand { Id = 1 };

            //Assert
            await Assert.ThrowsAsync<DeleteFailureException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void DeleteStudentCommand_ShouldThrowNotFoundException()
        { 
            //Arrange
            var sut = new DeleteStudentCommand.DeleteStudentCommandHandler(Context);
            var command = new DeleteStudentCommand { Id = 99 };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }
    }
}
