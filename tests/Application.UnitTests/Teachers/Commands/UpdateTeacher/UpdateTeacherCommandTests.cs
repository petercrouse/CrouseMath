using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CrouseMath.Application.UnitTests.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandTests : CommandTestBase
    {
        [Fact]
        public async Task UpdateTeacherCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var sut = new UpdateTeacherCommandHandler(Context);
            var command = new UpdateTeacherCommand
            {
                Id = 99,
                LastName = "The White"
            };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateTeacherCommand_ShouldUpdateTeacher()
        {
            //Arrange
            var sut = new UpdateTeacherCommandHandler(Context);
            var command = new UpdateTeacherCommand
            {
                Id = 1,
                LastName = "The White",
                FirstName = "Gandalf",
                Email = ""
            };

            //Act
            _ = await sut.Handle(command, CancellationToken.None);

            //Assert
            Context.Teachers.Find(command.Id).LastName.ShouldBe("The White");
        }
    }
}
