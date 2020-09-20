using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Students.Commands.UpdateStudent;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandTests : CommandTestBase
    {
        [Fact]
        public async Task UpdateStudentCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var sut = new UpdateStudentCommand.UpdateStudentCommandHandler(Context);
            var command = new UpdateStudentCommand {
                Id = 99,
                LastName = "Smeegol"
            };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateStudentCommand_ShouldUpdateStudent()
        {
            //Arrange
            var sut = new UpdateStudentCommand.UpdateStudentCommandHandler(Context);
            var command = new UpdateStudentCommand
            {
                Id = 1,
                LastName = "Smeegol",
                FirstName = "Frodo",
                Email = ""
            };

            //Act
            _ = await sut.Handle(command, CancellationToken.None);

            //Assert
            Context.Students.Find(command.Id).LastName.ShouldBe("Smeegol");
        }
    }
}
