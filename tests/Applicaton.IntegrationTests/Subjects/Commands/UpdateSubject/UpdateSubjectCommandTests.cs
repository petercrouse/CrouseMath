using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Subjects.Commands.UpdateSubject;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.Subjects.Commands.UpdateSubject
{
    public class UpdateSubjectCommandTests : CommandTestBase
    {
        [Fact]
        public async Task UpdateSubjectCommand_ShouldThrowNotFoundException()
        {
            //Arrange
            var sut = new UpdateSubjectCommand.UpdateSubjectCommandHandler(Context);
            var command = new UpdateSubjectCommand
            {
                Id = 99,
                Name = "VoidMagic"
            };

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCustomerCommand_ShouldUpdateSubject()
        {
            //Arrange
            var sut = new UpdateSubjectCommand.UpdateSubjectCommandHandler(Context);
            var command = new UpdateSubjectCommand
            {
                Id = 1,
                Name = "VoidMagic"
            };

            //Act
            _ = await sut.Handle(command, CancellationToken.None);

            //Assert
            Context.Subjects.Find(command.Id).Name.ShouldBe("VoidMagic");
        }
    }
}
