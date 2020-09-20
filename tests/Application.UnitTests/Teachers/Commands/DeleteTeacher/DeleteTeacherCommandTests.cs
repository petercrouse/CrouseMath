using System.Threading;
using Xunit;

namespace CrouseMath.Application.UnitTests.Teachers.Commands.DeleteTeacher
{
    public class DeleteTeacherCommandTests : CommandTestBase
    {
        [Fact]
        public async void DeleteTeacherCommand_ShouldThrowDeleteFailureException()
        {
            //Arrange
            var sut = new DeleteTeacherCommandHandler(Context);
            var command = new DeleteTeacherCommand { Id = 1 };

            //Assert
            await Assert.ThrowsAsync<DeleteFailureException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void DeleteTeacherCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var sut = new DeleteTeacherCommandHandler(Context);
            var command = new DeleteTeacherCommand { Id = 99 };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }
    }
}
