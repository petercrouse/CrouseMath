using System.Threading;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Subjects.Commands.DeleteSubject;
using Xunit;

namespace CrouseMath.Application.UnitTests.Subjects.Commands.DeleteSubject
{
    public class DeleteSubjectCommandTests : CommandTestBase
    {
        [Fact]
        public async void DeleteSubjectCommand_ShouldThrowDeleteFailureException()
        {
            //Arrange
            var sut = new DeleteSubjectCommand.DeleteSubjectCommandHandler(Context);
            var command = new DeleteSubjectCommand { Id = 1 };

            //Assert
            await Assert.ThrowsAsync<DeleteFailureException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void DeleteSubjectCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var sut = new DeleteSubjectCommand.DeleteSubjectCommandHandler(Context);
            var command = new DeleteSubjectCommand { Id = 99 };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }
    }
}
